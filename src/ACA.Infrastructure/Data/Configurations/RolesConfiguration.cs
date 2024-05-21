using ACA.Domain.RoleAggregate;
using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.HasKey(x=>x.Id);
  }
}
