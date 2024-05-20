using System.ComponentModel.DataAnnotations;

namespace ACA.Domain.Shared.Core;

/// <summary>
///   Base class for entities in the domain model without a specified identifier type.
/// </summary>
/// <remarks>
///   Author: Akbar Ahmadi Saray
/// </remarks>
public abstract class Entity : IEntity
{
    /// <summary>
    ///   NovinChecks if the entity is transient (not persisted).
    /// </summary>
    /// <returns>True if the entity is transient, otherwise false.</returns>
    public bool IsTransient()
    {
        return false; // Entities without a specific identifier type are never transient
    }

    /// <summary>
    ///   NovinChecks if the entity is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Entity && ReferenceEquals(this, obj);

        // Default implementation for non-generic Entity, can be overridden by subclasses
    }

    public abstract object?[] GetKeys();

    /// <summary>
    ///   Generates a hash code for the entity.
    /// </summary>
    /// <returns>A hash code for the entity.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode(); // Default implementation for non-generic Entity
    }
}

/// <summary>
///   Base class for entities in the domain model with a specified identifier type.
/// </summary>
/// <typeparam name="TId">The type of the identifier for the entity.</typeparam>
public abstract class Entity<TId> : Entity, IEntity<TId> where TId : IEquatable<TId>
{
    /// <summary>
    ///   Gets or sets the unique identifier of the entity.
    /// </summary>
    [Key]
    public virtual TId Id { get; set; } = default!;

    /// <summary>
    ///   NovinChecks if the entity is transient (not persisted).
    /// </summary>
    /// <returns>True if the entity is transient, otherwise false.</returns>
    public new bool IsTransient()
    {
        return Id.Equals(default); // Implementation specific to generic Entity<TId>
    }

    public override object?[] GetKeys()
    {
        return new object?[] { Id };
    }


    /// <summary>
    ///   NovinChecks if the entity is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other.IsTransient() || IsTransient())
        {
            return false;
        }

        return other.Id.Equals(Id);
    }

    /// <summary>
    ///   Generates a hash code for the entity.
    /// </summary>
    /// <returns>A hash code for the entity.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode(); // Implementation specific to generic Entity<TId>
    }
}