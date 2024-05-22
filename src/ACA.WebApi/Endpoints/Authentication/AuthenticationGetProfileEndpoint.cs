using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Application.Abstractions.UseCases.Authentication.Queries;
using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using FastEndpoints;
using MediatR;

namespace ACA.WebApi.Endpoints.Authentication;

public class AuthenticationGetProfileEndpoint(IMediator mediator)
  : EndpointWithoutRequest<GetAuthenticationProfileQueryResult>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/authentication/profile");
    Permissions("auth.profile");
  }

  public override async Task HandleAsync( CancellationToken ct)
  {
    
    var result = await _mediator.Send(new GetAuthenticationProfileQuery()
    {
      UserId = new Guid(User.Claims.FirstOrDefault(x=>x.Type == "Id")?.Value ?? string.Empty)
    }, ct);
    
    if (result.IsSuccess)
    {
      await SendAsync(result.Value, cancellation: ct);
    }
    else
    {
      if (result.Status == ResultStatus.Unauthorized)
      {
        await SendUnauthorizedAsync(ct);
      }
    }
  }
}
