using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class User : AggregateRoot<Guid>
{
    public UserPhoneNumber PhoneNumber { get; set; } = default!;

    public UserStatus Status { get; set; } = UserStatus.Active;

    public UserProfile? Profile { get; set; }
    public virtual ICollection<Role> Roles { get; set; } = [];

    // Constructor for EF
    protected User()
    {
    }

    private User(UserProfile profile, UserPhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
    }

    public static User? Create(string firstName, string lastName, string phoneNumberCode, string phoneNumber)
    {
        var profile = new UserProfile(firstName, lastName);
        var phone = new UserPhoneNumber(phoneNumberCode, phoneNumber);
        return new User(profile, phone);
    }
}