using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Common.Result;
using ACA.Domain.Shared.Core;
using ACA.Domain.VerificationAggregate;
using MediatR;

namespace ACA.Application.UseCases.Authentication.Commands;

public class SendAuthenticationCodeCommandHandler(IRepository<Verification> verificationRepository)
  : IRequestHandler<SendAuthenticationCodeCommand,Result>
{
  public async Task<Result> Handle(SendAuthenticationCodeCommand request, CancellationToken cancellationToken)
  {
    var verification = await verificationRepository.FindOneAsync(x => x.Type == VerificationType.Phone && x.Key.Equals(request.PhoneNumber.Number), cancellationToken);

    if (verification != null)
    {
      return Result.Error($"verification code is not found");
    }

    verification = new Verification(VerificationType.Phone,request.PhoneNumber.Number!,"123456");
    await verificationRepository.InsertAsync(verification, cancellationToken: cancellationToken);
    return Result.Success();
  }
}
