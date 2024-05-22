using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using ACA.Domain.VerificationAggregate;
using FastEndpoints.Security;
using MediatR;

namespace ACA.Application.UseCases.Authentication.Commands;

public class VerifyAuthenticationCodeCommandHandler(
    IRepository<Verification> verificationRepository,
    IRepository<User> userRepository)
    : IRequestHandler<VerifyAuthenticationCodeCommand, Result<VerifyAuthenticationCodeResult>>
{
    public async Task<Result<VerifyAuthenticationCodeResult>> Handle(
        VerifyAuthenticationCodeCommand request,
        CancellationToken cancellationToken)
    {
        var verification = await verificationRepository.FindOneAsync(
            x => x.Type == VerificationType.Phone && x.Key == request.PhoneNumber.Number, cancellationToken);

        if (verification == null)
        {
            return Result.Unauthorized();
        }

        if (verification.ValidateValue(request.Code))
        {
            return Result.Unauthorized();
        }

        await verificationRepository.DeleteAsync(verification, cancellationToken: cancellationToken);

        bool isNewUser = false;

        var user = await userRepository.FindOneAsync(x => x.UserName.Equals(request.PhoneNumber.Number),
            cancellationToken);

        if (user == null)
        {
            isNewUser = true;
            user = new User()
            {
                UserName = request.PhoneNumber.Number,
                Profile = new UserProfile() { PhoneNumber = request.PhoneNumber, Status = UserStatus.Active },
            };
            user = await userRepository.InsertAsync(user, cancellationToken: cancellationToken);
        }

        var refreshToken = Guid.NewGuid().ToString();

        await verificationRepository.DeleteAsync(x =>
                x.Type == VerificationType.RefreshToken && x.Key.Equals(user.Id.ToString()),
            cancellationToken: cancellationToken);

        await verificationRepository.InsertAsync(
            new Verification(VerificationType.RefreshToken, user.Id.ToString(), refreshToken),
            cancellationToken: cancellationToken);

        var expiredAt = DateTime.UtcNow.AddDays(1);
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "asdadknwkjernjkwqbrjhwebjrhbwjerwjker";
                o.ExpireAt = expiredAt;
                o.User.Permissions.Add();
                o.User.Claims.Add(
                    ("UserName", user.UserName),
                    ("Id", user.Id.ToString())
                );
                o.User.Permissions.Add(("auth.profile"));
            });

        return new VerifyAuthenticationCodeResult()
        {
            IsNew = isNewUser, RefreshToken = refreshToken, AccessToken = jwtToken, ExpiredAt = expiredAt.ToLocalTime()
        };
    }
}