using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using MediatR;

namespace ACA.Application.Abstractions.UseCases.Authentication.Commands;

public class SendAuthenticationCodeCommand : IRequest<Result>
{
  public UserPhoneNumber PhoneNumber { get; set; } = default!;
}