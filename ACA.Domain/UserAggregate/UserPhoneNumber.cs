using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class UserPhoneNumber : ValueObject
{
    public UserPhoneNumber(string number)
    {
        Number = number;
    }

    public string Number { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}