using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Domain.UserAggregate;
using FastEndpoints;
using FluentValidation;
using MediatR;

namespace ACA.WebApi.Endpoints.Authentication;


public class AuthenticationSendCodeEndpoint(IMediator mediator) : Endpoint<SendAuthenticationCodeCommand>
{

  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    AllowAnonymous();
    Post("/authentication/send");
    Summary(c =>
    {
      c.ExampleRequest = new SendAuthenticationCodeCommand()
      {
        PhoneNumber = new UserPhoneNumber("09371770774")
      };
    });
  }

  public override async Task HandleAsync(SendAuthenticationCodeCommand req, CancellationToken ct)
  {
   
    var result = await _mediator.Send(req, ct);
    if (result.IsSuccess)
    {
      await SendAsync(result.Value, cancellation: ct);
    }
  }
}


