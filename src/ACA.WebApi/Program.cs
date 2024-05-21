using ACA.Application.Abstractions.UseCases.Authentication.Commands;
using ACA.Application.UseCases.Authentication.Commands;
using ACA.Infrastructure.Ioc;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MediatR;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints().SwaggerDocument();
builder.Services.AddAuthenticationJwtBearer(c => c.SigningKey = "asdadknwkjernjkwqbrjhwebjrhbwjerwjker");
builder.Services.AddAuthorization();



ConfigureMediatr(builder);



builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("Default"));

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
