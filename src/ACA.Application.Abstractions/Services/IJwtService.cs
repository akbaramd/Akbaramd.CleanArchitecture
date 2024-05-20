using ACA.Domain.UserAggregate;

namespace ACA.Application.Abstractions.Services;

public interface IJwtService
{
    public string Generate(User user);
    public bool Validate(string token);
}