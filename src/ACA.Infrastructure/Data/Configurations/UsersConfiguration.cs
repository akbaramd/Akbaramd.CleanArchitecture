using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique(); // Assuming phone numbers must be unique

            builder.Property(p => p.Status)
                .HasColumnName(nameof(User.Status))
                .HasConversion(
                    status => status.Id,
                    id => Enumeration.FromId<UserStatus>(id))
                .IsRequired();
            
            builder.OwnsOne(u => u.Profile, profileBuilder =>
            {
                profileBuilder.Property(p => p.FirstName)
                    .HasColumnName(nameof(UserProfile.FirstName))
                    .IsRequired();

                profileBuilder.Property(p => p.LastName)
                    .HasColumnName(nameof(UserProfile.LastName))
                    .IsRequired();
            });

            builder.OwnsOne(u => u.PhoneNumber, phoneNumberBuilder =>
            {
                phoneNumberBuilder.Property(p => p.Code)
                    .HasColumnName(nameof(UserPhoneNumber.Code))
                    .IsRequired();

                phoneNumberBuilder.Property(p => p.Number)
                    .HasColumnName(nameof(UserPhoneNumber.Number))
                    .IsRequired();

                phoneNumberBuilder.HasIndex(p => p.Number)
                    .IsUnique(); // Assuming phone numbers must be unique
            });

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"))
                .ToTable("UserRoles");

        }
    }
}