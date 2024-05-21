using ACA.Domain.Shared.Core;

namespace ACA.Domain.PermissionAggregate;

public class Permission : AggregateRoot<Guid>
{
    public string Title { get; set; } = default!;
    public string Name { get; set; } = default!;
    
    public override object?[] GetKeys()
    {
      return [Name];
    }
}