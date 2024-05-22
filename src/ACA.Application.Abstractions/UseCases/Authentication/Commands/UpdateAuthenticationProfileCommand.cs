using System.Text.Json.Serialization;
using ACA.Common.Result;
using FastEndpoints;
using MediatR;

namespace ACA.Application.Abstractions.UseCases.Authentication.Commands;

public class UpdateAuthenticationProfileCommand : IRequest<Result>
{
  [JsonIgnore]
  [HideFromDocs]
  public Guid Id { get; set; }
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;
  public string Email { get; set; } = default!;
}

