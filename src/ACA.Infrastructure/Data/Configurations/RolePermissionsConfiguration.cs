using ACA.Domain.RoleAggregate;
using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x=>x.Id);
    
       
        builder.HasOne(x => x.Permission).WithMany().HasForeignKey(x=>x.PermissionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Role).WithMany().HasForeignKey(x=>x.RoleId).OnDelete(DeleteBehavior.Cascade);
    }
}