using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using ACA.Domain.VerificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACA.Infrastructure.Data.Configurations;

public class VerificationConfiguration : IEntityTypeConfiguration<Verification>
{
    public void Configure(EntityTypeBuilder<Verification> builder)
    {
        builder.HasIndex(c => c.Key);
        builder
          .Property(x => x.Type)
          .HasColumnName(nameof(Verification.Type))
          .HasConversion(c => c.Id, v => Enumeration.FromId<VerificationType>(v));
    }
}