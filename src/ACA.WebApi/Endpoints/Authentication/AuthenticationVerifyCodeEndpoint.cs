using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using FastEndpoints;
using MediatR;

namespace ACA.WebApi.Endpoints.Authentication;

public class AuthenticationVerifyCodeEndpoint(IMediator mediator)
  : Endpoint<VerifyAuthenticationCodeCommand, VerifyAuthenticationCodeResult>
{
  public override void Configure()
  {
    AllowAnonymous();
    Post("/authentication/verify");
    Summary(summary =>
    {
      summary.ExampleRequest = new VerifyAuthenticationCodeCommand
      {
        PhoneNumber = new UserPhoneNumber("98", "09371770774"),
        Code = "123456"
      };
    });
  }

  public override async Task HandleAsync(VerifyAuthenticationCodeCommand req, CancellationToken ct)
  {
    var result = await mediator.Send(req, ct);
    if (result.IsSuccess)
    {
      await SendAsync(result.Value, cancellation: ct);
    }
    else if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
  }
}
