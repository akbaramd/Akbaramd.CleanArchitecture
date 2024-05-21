using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;

namespace ACA.Domain.VerificationAggregate;

public class VerificationType : Enumeration
{
    public static VerificationType Phone = new VerificationType(0, nameof(Phone));
    public static VerificationType Email = new VerificationType(1, nameof(Email));
    public static VerificationType RefreshToken = new VerificationType(2, nameof(RefreshToken));

    public VerificationType(int id, string name) : base(id, name)
    {
    }
}