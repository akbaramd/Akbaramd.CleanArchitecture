using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.HasKey(x=>x.RoleId);
    builder.HasKey(x=>x.UserId);

    builder.HasOne(x => x.User).WithMany(x => x.Roles).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    builder.HasOne(x => x.Role).WithMany(x=>x.Roles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
  }
}
