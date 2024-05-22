using ACA.Domain.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.HasKey(x=>x.Id);
    
    
    builder.HasMany(x=>x.Permissions).WithMany(x=>x.Roles).UsingEntity(c =>
    {
      c.ToTable("RolePermissions");
    });
  }
}
