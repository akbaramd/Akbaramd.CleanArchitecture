using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Application.UseCases.Authentication.Commands;
using ACA.Infrastructure.Ioc;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints().SwaggerDocument(o =>
{
  o.EnableJWTBearerAuth = false;
  o.DocumentSettings = s =>
  {
    s.DocumentName = "ACA Api Webservice";
    s.Title = "ACA API";
    s.Version = "v1.0";
    s.AddAuth("Bearer", new()
    {
      Type = OpenApiSecuritySchemeType.Http,
      Scheme = JwtBearerDefaults.AuthenticationScheme,
      BearerFormat = "JWT",
    });
  };
});
builder.Services.AddAuthenticationJwtBearer(c => c.SigningKey = "asdadknwkjernjkwqbrjhwebjrhbwjerwjker");
builder.Services.AddAuthorization();
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("Default"));

ConfigureMediatr(builder);


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints().UseSwaggerGen();



app.Run();


void ConfigureMediatr(WebApplicationBuilder webApplicationBuilder)
{
  webApplicationBuilder.Services.AddMediatR(c =>
  {
    c.RegisterServicesFromAssembly(typeof(SendAuthenticationCodeCommand).Assembly);
    c.RegisterServicesFromAssembly(typeof(SendAuthenticationCodeCommandHandler).Assembly);
  });
}
