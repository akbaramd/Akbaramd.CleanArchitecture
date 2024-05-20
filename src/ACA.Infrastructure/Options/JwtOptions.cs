namespace ACA.Infrastructure.Options;

public class JwtOptions
{
    public const string Key = "jwt";

    public string SecretKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public long ExpiredAt { get; set; }
}