using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using FastEndpoints;
using FluentValidation;

namespace ACA.WebApi.Endpoints.Authentication;

public class AuthenticationVerifyCodeEndpointValidator : Validator<SendAuthenticationCodeCommand>
{
  public AuthenticationVerifyCodeEndpointValidator()
  {
    RuleFor(x => x.PhoneNumber).NotNull();
    RuleFor(x => x.PhoneNumber.Number).NotNull();
  }
}
