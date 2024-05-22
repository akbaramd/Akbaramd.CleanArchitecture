using System;
using System.Collections.Generic;

namespace ACA.Domain.Shared.Core
{
    /// <summary>
    /// Base class for aggregate roots, handling domain events.
    /// </summary>
    /// <remarks>
    /// Author: AkbarAmd
    /// </remarks>
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        /// <summary>
        /// Gets the list of domain events associated with the entity.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the list associated with the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the list associated with the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    /// <summary>
    /// Base class for aggregate roots with a specified identifier type, handling domain events.
    /// </summary>
    /// <typeparam name="TId">The type of the identifier for the aggregate root.</typeparam>
    /// <remarks>
    /// Author: AkbarAmd
    /// </remarks>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId> where TId : IEquatable<TId>
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        /// <summary>
        /// Gets the list of domain events associated with the entity.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a domain event to the list associated with the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the list associated with the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
