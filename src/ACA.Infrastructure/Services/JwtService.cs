using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ACA.Application.Abstractions.Services;
using ACA.Domain.UserAggregate;
using ACA.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ACA.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_jwtOptions.Issuer,
            _jwtOptions.Audience,
            new []
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Profile?.Email??string.Empty),
                new Claim(ClaimTypes.MobilePhone,user.Profile?.PhoneNumber?.Number??string.Empty),
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
            },
            expires: DateTime.Now.AddSeconds(_jwtOptions.ExpiredAt),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool Validate(string token)
    {
        throw new NotImplementedException();
    }
}