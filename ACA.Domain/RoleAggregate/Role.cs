using ACA.Domain.PermissionAggregate;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;

namespace ACA.Domain.RoleAggregate;

public class Role : AggregateRoot<Guid>
{
  public Role(string name, string title, bool deletable = true)
  {
    Name = name;
    Title = title;
    Deletable = deletable;
  }

  public string Name { get; set; }
  public string Title { get; set; }
  public bool Deletable { get; set; } = false;
  
  public ICollection<User> Users { get; set; } = [];

  public virtual ICollection<Permission> Permissions { get; set; } = [];
 
}
