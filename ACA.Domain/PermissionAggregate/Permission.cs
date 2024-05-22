using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;

namespace ACA.Domain.PermissionAggregate;

public class Permission : AggregateRoot<Guid>
{
  public Permission( string name,string title, string group = "default")
  {
    Title = title;
    Name = name;
    Group = group;
  }

  public string Title { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Group { get; set; } = default!;
    
    public ICollection<Role> Roles { get; set; } = [];
    public override object?[] GetKeys()
    {
      return [Name];
    }


    public static implicit operator string(Permission permission) => permission.Name;
    public static implicit operator Guid(Permission permission) => permission.Id;
}