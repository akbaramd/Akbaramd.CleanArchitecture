using ACA.Common.Result;
using ACA.Domain.UserAggregate;
using MediatR;

namespace ACA.Application.Abstractions.UseCases.Authentication.Commands;

public class VerifyAuthenticationCodeCommand : IRequest<Result<VerifyAuthenticationCodeResult>>
{
  public UserPhoneNumber PhoneNumber { get; set; } = default!;
  public string Code { get; set; } = default!;
}

public class VerifyAuthenticationCodeResult 
{
  public string AccessToken { get; set; } = default!;
  public string RefreshToken { get; set; } = default!;
  public DateTime ExpiredAt { get; set; } = default!;
  public bool IsNew { get; set; } = false;
}