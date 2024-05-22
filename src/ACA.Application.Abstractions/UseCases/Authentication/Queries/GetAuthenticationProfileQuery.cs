using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using MediatR;

namespace ACA.Application.Abstractions.UseCases.Authentication.Queries;

public class GetAuthenticationProfileQuery : IRequest<Result<GetAuthenticationProfileQueryResult>>
{
    public Guid UserId { get; set; }   
}

public class GetAuthenticationProfileQueryResult 
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public UserProfile Profile { get; set; } = default!;
}