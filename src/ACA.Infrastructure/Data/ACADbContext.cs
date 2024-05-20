using ACA.Domain.RoleAggregate;
using ACA.Domain.UserAggregate;
using ACA.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACA.Infrastructure.Data;

public class ACADbContext : DbContext
{
    public ACADbContext(DbContextOptions<ACADbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}