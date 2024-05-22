using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class UserProfile : ValueObject
{
  public UserProfile()
  {
  }
    public UserStatus Status { get; set; } = UserStatus.Active;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public UserPhoneNumber PhoneNumber { get; set; } = default!;


    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (FirstName != null) yield return FirstName;
        if (LastName != null) yield return LastName;
    }
}