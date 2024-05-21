using ACA.Domain.PermissionAggregate;
using ACA.Domain.RoleAggregate;
using ACA.Domain.UserAggregate;
using ACA.Domain.VerificationAggregate;
using ACA.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACA.Infrastructure.Data;

public class ACADbContext : DbContext
{
    public ACADbContext(DbContextOptions<ACADbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserRole> UsersRoles { get; set; } = default!;
    public DbSet<Role> Roles { get; set; }= default!;
    public DbSet<Permission> Permissions { get; set; }= default!;
    public DbSet<RolePermission> RolePermissions  { get; set; }= default!;
    public DbSet<Verification> Verifications  { get; set; }= default!;
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UsersConfiguration).Assembly);
    }
}