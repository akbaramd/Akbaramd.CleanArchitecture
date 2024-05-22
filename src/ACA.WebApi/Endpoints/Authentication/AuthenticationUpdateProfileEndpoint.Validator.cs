using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using FastEndpoints;
using FluentValidation;

namespace ACA.WebApi.Endpoints.Authentication;

public class AuthenticationUpdateProfileEndpointValidator : Validator<AuthenticationUpdateProfileEndpoint>
{
  public AuthenticationUpdateProfileEndpointValidator()
  {
  }
}
