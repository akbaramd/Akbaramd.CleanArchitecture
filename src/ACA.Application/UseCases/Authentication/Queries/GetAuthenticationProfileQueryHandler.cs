using ACA.Application.Abstractions.UseCases.Authentication.Queries;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using MediatR;

namespace ACA.Application.UseCases.Authentication.Queries;

public class GetAuthenticationProfileQueryHandler (IRepository<User> repository) : IRequestHandler<GetAuthenticationProfileQuery,Result<GetAuthenticationProfileQueryResult>>
{
    
    
    
    public async Task<Result<GetAuthenticationProfileQueryResult>> Handle(GetAuthenticationProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.FindOneAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Unauthorized();
        }

        return new GetAuthenticationProfileQueryResult()
        {
            Id = user.Id,
            UserName = user.UserName,
            Profile = user.Profile
        };
    }
}