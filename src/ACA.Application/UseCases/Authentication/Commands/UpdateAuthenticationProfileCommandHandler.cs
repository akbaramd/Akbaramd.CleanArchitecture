using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using ACA.Domain.VerificationAggregate;
using MediatR;

namespace ACA.Application.UseCases.Authentication.Commands;

public class UpdateAuthenticationProfileCommandHandler(IRepository<User> repository)
  : IRequestHandler<UpdateAuthenticationProfileCommand,Result>
{
  public async Task<Result> Handle(UpdateAuthenticationProfileCommand request, CancellationToken cancellationToken)
  {
    var user = await repository.FindOneAsync(x => x.Id.Equals(request.Id), cancellationToken);

    if (user == null)
    {
      return Result.Unauthorized();
    }

    user.Profile.FirstName = request.FirstName;
    user.Profile.LastName = request.LastName;
    user.Profile.Email = request.Email;

    await repository.UpdateAsync(user, cancellationToken: cancellationToken);
    
    return Result.Success();
  }
}
