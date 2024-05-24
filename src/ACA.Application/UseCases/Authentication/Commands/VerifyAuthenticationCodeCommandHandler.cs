using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using ACA.Domain.VerificationAggregate;
using FastEndpoints.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACA.Application.UseCases.Authentication.Commands
{
    public class VerifyAuthenticationCodeCommandHandler : IRequestHandler<VerifyAuthenticationCodeCommand, Result<VerifyAuthenticationCodeResult>>
    {
        private readonly IRepository<Verification> _verificationRepository;
        private readonly IRepository<User> _userRepository;

        public VerifyAuthenticationCodeCommandHandler(IRepository<Verification> verificationRepository, IRepository<User> userRepository)
        {
            _verificationRepository = verificationRepository ?? throw new ArgumentNullException(nameof(verificationRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result<VerifyAuthenticationCodeResult>> Handle(VerifyAuthenticationCodeCommand request, CancellationToken cancellationToken)
        {
            var verification = await GetVerificationAsync(request.PhoneNumber, cancellationToken);
            if (verification == null || !verification.ValidateValue(request.Code))
            {
                return Result.Unauthorized();
            }

            await _verificationRepository.DeleteAsync(verification,true, cancellationToken);

            var user = await GetUserAsync(request.PhoneNumber, cancellationToken);
            bool isNewUser = user == null;

            if (isNewUser)
            {
                user = await CreateUserAsync(request.PhoneNumber, cancellationToken);
            }

            var refreshToken = await CreateRefreshTokenAsync(user!, cancellationToken);

            var jwtToken = GenerateJwtToken(user!, refreshToken);

            return new VerifyAuthenticationCodeResult
            {
                IsNew = isNewUser,
                RefreshToken = refreshToken,
                AccessToken = jwtToken,
                ExpiredAt = DateTime.UtcNow.AddDays(1).ToLocalTime()
            };
        }

        private async Task<Verification?> GetVerificationAsync(UserPhoneNumber phoneNumber, CancellationToken cancellationToken)
        {
            return await _verificationRepository.FindOneAsync(
                x => x.Type == VerificationType.Phone && x.Key == phoneNumber.Number, cancellationToken);
        }

        private async Task<User?> GetUserAsync(UserPhoneNumber phoneNumber, CancellationToken cancellationToken)
        {
            return await _userRepository.Queryable()
                .Include(c => c.Roles)
                .ThenInclude(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.PhoneNumber.Equals(phoneNumber), cancellationToken);
        }

        private async Task<User> CreateUserAsync(UserPhoneNumber phoneNumber, CancellationToken cancellationToken)
        {
            var newUser = User.Create("FirstName", "LastName", phoneNumber.Code, phoneNumber.Number);
            newUser.Status = UserStatus.Active;
            return await _userRepository.InsertAsync(newUser, true,cancellationToken);
        }

        private async Task<string> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken)
        {
            var refreshToken = Guid.NewGuid().ToString();

            await _verificationRepository.DeleteAsync(x =>
                    x.Type == VerificationType.RefreshToken && x.Key.Equals(user.Id.ToString()),
                cancellationToken: cancellationToken);

            var newRefreshToken = new Verification(VerificationType.RefreshToken, user.Id.ToString(), refreshToken);
            await _verificationRepository.InsertAsync(newRefreshToken, true,cancellationToken);

            return refreshToken;
        }

        private string GenerateJwtToken(User user, string refreshToken)
        {
            var expiredAt = DateTime.UtcNow.AddDays(1);
            return JwtBearer.CreateToken(
                o =>
                {
                    o.SigningKey = "asdadknwkjernjkwqbrjhwebjrhbwjerwjker";
                    o.ExpireAt = expiredAt;
                    o.User.Roles.Add(user.Roles.Select(x => x.Name).Distinct().ToArray());
                    o.User.Permissions.Add(user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name).Distinct().ToArray());
                    o.User.Claims.Add(
                        ("PhoneNumber", user.PhoneNumber.ToString()),
                        ("Id", user.Id.ToString())
                    );
                });
        }
    }
}
