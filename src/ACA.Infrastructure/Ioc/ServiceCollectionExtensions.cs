using ACA.Domain.Services;
using ACA.Domain.Shared.Core;
using ACA.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ACA.Infrastructure.Ioc;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDomain(this IServiceCollection services)
  {
    services.AddScoped<UserManager>();
    return services;
  }

  public static IServiceCollection AddDataAccess(this IServiceCollection services, string? connectionString)
  {

    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentNullException(nameof(connectionString),
        "Connection string cannot be empty . please add connection string in builder.Services.AddDataAccess(...)");
    }
    
    services.AddDbContext<ACADbContext>(c =>
    {
      c.UseSqlServer(connectionString);
    });
    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
    return services;
  }
}
