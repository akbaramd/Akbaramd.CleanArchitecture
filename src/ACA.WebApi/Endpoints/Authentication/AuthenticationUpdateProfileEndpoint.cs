using ACA.Application.Abstractions.UseCases.Authentication;
using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using FastEndpoints;
using MediatR;

namespace ACA.WebApi.Endpoints.Authentication;


public class AuthenticationUpdateProfileEndpoint(IMediator mediator) : Endpoint<UpdateAuthenticationProfileCommand>
{

  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    
    Put("/authentication/profile");
    Permissions(AuthenticationConstants.UpdateProfilePermission);
    Summary(c =>
    {
      c.ExampleRequest = new UpdateAuthenticationProfileCommand()
      {
        FirstName = "Akbar",
        LastName = "Akbar",
        Email = "akbarsafari00@gmail.com"
      };
    });
  }

  public override async Task HandleAsync(UpdateAuthenticationProfileCommand req, CancellationToken ct)
  {

    var userId = new Guid(User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? string.Empty);
    req.Id = userId;
    var result = await _mediator.Send(req, ct);
    if (result.IsSuccess)
    {
      await SendAsync(result.Value, cancellation: ct);
    }
  }
}


