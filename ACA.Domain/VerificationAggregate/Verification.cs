using ACA.Domain.Shared.Core;

namespace ACA.Domain.VerificationAggregate;

public class Verification : AggregateRoot<Guid>
{
  public Verification(VerificationType type, string key, string value)
  {
    Type = type;
    Key = key;
    SetValue(value);
  }

  public VerificationType Type { get; private set; } = default!;
  public string Key { get;private  set; } = default!;
  public string Value { get;private  set; } = default!;

  public void SetValue(string value)
  {
    var encDataByte = System.Text.Encoding.UTF8.GetBytes(value);
    string encodedData = Convert.ToBase64String(encDataByte);
    Value = encodedData;
  }
  
  public bool ValidateValue(string value)
  {
    var encDataByte = System.Text.Encoding.UTF8.GetBytes(value);
    string encodedData = Convert.ToBase64String(encDataByte);
    return Value == encodedData;
  }
}
