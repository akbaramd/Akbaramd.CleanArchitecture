using System.Reflection;
using System.Text;
using ACA.Application.Abstractions.Services;
using ACA.Infrastructure.Options;
using ACA.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ACA.Infrastructure.Ioc;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddJwtBearer(AuthenticationBuilder builder)
    {
        builder.Services.AddScoped<IJwtService, JwtService>();
        
        builder.Services.Configure<JwtOptions>(JwtOptions.Key,c =>
        {
            c.SecretKey = Assembly.GetExecutingAssembly().FullName + Guid.NewGuid().ToString("N");
            c.ExpiredAt = 60000;
            c.Issuer = "localhost";
            c.Audience = "localhost";
        });


        var jwtOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptionsSnapshot<JwtOptions>>().Value;
        
        builder
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, "Bearer", x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                            .GetBytes(jwtOptions!.SecretKey)
                    ),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issuer,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return builder;
    }
}