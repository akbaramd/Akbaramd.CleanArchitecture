using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
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

    user.Profile = new UserProfile(request.FirstName, request.LastName);

    await repository.UpdateAsync(user, cancellationToken: cancellationToken);
    
    return Result.Success();
  }
}
