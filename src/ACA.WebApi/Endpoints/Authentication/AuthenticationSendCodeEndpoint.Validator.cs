using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using FastEndpoints;
using FluentValidation;

namespace ACA.WebApi.Endpoints.Authentication;

public class AuthenticationSendCodeEndpointValidator : Validator<VerifyAuthenticationCodeCommand>
{
  public AuthenticationSendCodeEndpointValidator()
  {
    RuleFor(x => x.PhoneNumber).NotNull();
    RuleFor(x => x.PhoneNumber.Number).NotNull();
    RuleFor(x => x.Code).NotNull();
  }
}
