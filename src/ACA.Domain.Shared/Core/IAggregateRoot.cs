namespace ACA.Domain.Shared.Core;

public interface IAggregateRoot : IEntity
{
}

public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
{
}