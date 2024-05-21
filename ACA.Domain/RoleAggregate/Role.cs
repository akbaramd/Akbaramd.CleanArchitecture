using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace ACA.Domain.RoleAggregate;

public class Role : AggregateRoot<Guid>
{
  public string Title { get; set; } = default!;
  public string Name { get; set; } = default!;
  
  public ICollection<UserRole> Roles { get; set; } = [];

  public ICollection<RolePermission> Permissions { get; set; } = [];


 
}
