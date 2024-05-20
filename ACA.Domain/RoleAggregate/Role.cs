using ACA.Domain.Shared.Core;
using Microsoft.AspNetCore.Identity;

namespace ACA.Domain.RoleAggregate;

public class Role : AggregateRoot<Guid>
{
    public string Title { get; set; } = default!;
    public string Name { get; set; } = default!;
}