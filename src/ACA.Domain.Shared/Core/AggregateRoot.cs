namespace ACA.Domain.Shared.Core;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    private List<IDomainEvent> _domainEvents = [];

    /// <summary>
    ///   Gets the list of domain events associated with the entity.
    /// </summary>
    public List<IDomainEvent> DomainEvents => _domainEvents ??= new List<IDomainEvent>();

    /// <summary>
    ///   Adds a domain event to the list associated with the entity.
    /// </summary>
    /// <param name="domainEventItem">The domain event to add.</param>
    public void AddDomainEvent(IDomainEvent domainEventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEventItem);
    }

    /// <summary>
    ///   Removes a domain event from the list associated with the entity.
    /// </summary>
    /// <param name="domainEventItem">The domain event to remove.</param>
    public void RemoveDomainEvent(IDomainEvent domainEventItem)
    {
        _domainEvents?.Remove(domainEventItem);
    }


    /// <summary>
    ///   Clears all domain events associated with the entity.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId> where TId : IEquatable<TId>
{
}