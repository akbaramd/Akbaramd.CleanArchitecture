using ACA.Domain.UserAggregate;

namespace ACA.Domain.Services;

public class UserManager
{
  public void GeneratePassword(User user, string password)
  {
    byte[] encData_byte;
    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
    string encodedData = Convert.ToBase64String(encData_byte);
    user.Password = encodedData;
  }

  public bool ValidatePassword(User user, string password)
  {
    byte[] encData_byte;
    encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
    string encodedData = Convert.ToBase64String(encData_byte);
    return user.Password == encodedData;
  }
}
