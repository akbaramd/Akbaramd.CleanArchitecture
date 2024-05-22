using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate;

public class User : AggregateRoot<Guid>
{
  public string UserName { get; set; } = default!;
  public string? Password { get; set; } 
  public UserProfile Profile { get; set; } = new ();
  public virtual ICollection<Role> Roles { get; set; } = [];

}