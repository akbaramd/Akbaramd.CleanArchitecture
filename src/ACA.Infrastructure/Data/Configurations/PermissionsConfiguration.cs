using ACA.Domain.PermissionAggregate;
using ACA.Domain.RoleAggregate;
using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class PermissionsConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.HasKey(x=>x.Id);
  }
}
