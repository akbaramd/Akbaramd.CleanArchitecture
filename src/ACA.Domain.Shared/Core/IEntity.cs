namespace ACA.Domain.Shared.Core;

public interface IEntity
{
    object?[] GetKeys();
}

public interface IEntity<TId> : IEntity
{
    TId Id { get; set; }
}