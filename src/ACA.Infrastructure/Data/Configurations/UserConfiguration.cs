using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(c => c.UserName);
        
        builder.OwnsOne(x => x.Profile, ctx =>
        {

            ctx.HasIndex(c => c.Email);
            
            ctx
                .Property(x => x.FirstName)
                .HasColumnName(nameof(UserProfile.FirstName));

            ctx
                .Property(x => x.LastName)
                .HasColumnName(nameof(UserProfile.LastName));
            
            ctx
                .Property(x => x.Email)
                .HasColumnName(nameof(UserProfile.Email));

            ctx.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.HasIndex(x => x.Number);
                nb
                    .Property(x => x.Number)
                    .HasColumnName(nameof(UserProfile.PhoneNumber));
            });
                

            ctx
                .Property(x => x.Status)
                .HasColumnName(nameof(UserProfile.Status))
                .HasConversion(c => c.Id, v => UserStatus.FromId(v));
        });

        builder.HasMany(x => x.Roles).WithMany();
    }
}