using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class UserStatus : Enumeration
{
    public static UserStatus UnActive = new UserStatus(0, nameof(UnActive));
    public static UserStatus Active = new UserStatus(1, nameof(Active));
    public static UserStatus Banned = new UserStatus(2, nameof(Banned));

    public UserStatus(int id, string name) : base(id, name)
    {
    }
}