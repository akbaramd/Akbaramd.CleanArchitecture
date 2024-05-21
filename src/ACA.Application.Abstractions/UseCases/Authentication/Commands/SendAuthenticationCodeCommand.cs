using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using FluentValidation;
using MediatR;

namespace ACA.Application.Abstractions.UseCases.Authentication.Commands;

public class SendAuthenticationCodeCommand : IRequest<Result>
{
  public UserPhoneNumber PhoneNumber { get; set; } = default!;
}

public class SendVerificationCodeCommandValidator : AbstractValidator<SendAuthenticationCodeCommand>
{
  public SendVerificationCodeCommandValidator()
  {
    RuleFor(x => x.PhoneNumber).NotNull();
    RuleFor(x => x.PhoneNumber.Number).NotNull();
  }
}
