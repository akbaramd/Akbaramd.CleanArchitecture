using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class UserRole : Entity
{
  public Guid UserId { get; set; }
  public User User { get; set; } = default!;
  public Guid RoleId{ get; set; } 
  public Role Role { get; set; } = default!;


  
  public override object?[] GetKeys()
  {
    return [UserId, RoleId];
  }
}
