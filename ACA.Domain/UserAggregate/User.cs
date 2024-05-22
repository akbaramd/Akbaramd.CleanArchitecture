using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;
using Microsoft.AspNetCore.Identity;

namespace ACA.Domain.UserAggregate;

public class User : AggregateRoot<Guid>
{
  public string UserName { get; set; } = default!;
  public string? Password { get; set; } 
  public UserProfile Profile { get; set; } = new ();
  public ICollection<UserRole> Roles { get; set; } = [];

}