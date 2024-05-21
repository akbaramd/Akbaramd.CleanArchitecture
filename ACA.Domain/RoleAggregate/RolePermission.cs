using ACA.Domain.PermissionAggregate;
using ACA.Domain.Shared.Core;
using ACA.Domain.UserAggregate;

namespace ACA.Domain.RoleAggregate;

public class RolePermission : Entity<Guid>
{
  public Guid RoleId { get; set; } 
  public Role Role { get; set; } = default!;
  public Guid PermissionId { get; set; } 
  public Permission Permission { get; set; } = default!;

 
}
