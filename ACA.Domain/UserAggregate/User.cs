using ACA.Domain.RoleAggregate;
using ACA.Domain.Shared.Core;
using Microsoft.AspNetCore.Identity;

namespace ACA.Domain.UserAggregate;

public class User : AggregateRoot<Guid>
{
    public string UserName { get; set; }
    public UserProfile Profile { get; set; } = default!;
    public ICollection<Role> Roles { get; set; } = [];


    public void AssignRole(params string[] roles)
    {
        foreach (var role in roles)
        {
            Roles.Add(new Role(){Name = role,Title = role});
        }
    }
    
    public void RemoveRole(params string[] roles)
    {
        foreach (var role in roles)
        {
            var find = Roles.FirstOrDefault(x => x.Name.Equals(roles));
            if (find != null) Roles.Remove(find);
        }
    }
    
}