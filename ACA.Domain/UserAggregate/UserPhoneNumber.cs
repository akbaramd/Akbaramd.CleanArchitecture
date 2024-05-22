using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class UserPhoneNumber : ValueObject
{
    public UserPhoneNumber(string code,string number)
    {
      Number = number;
      Code = code;
    }

    public string Code { get; set; }
    public string Number { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}