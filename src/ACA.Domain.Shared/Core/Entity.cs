namespace ACA.Domain.Shared.Core
{
  /// <summary>
  /// Base class for entities without a specified identifier type.
  /// </summary>
  /// <remarks>
  /// Author: AkbarAmd
  /// </remarks>
  public abstract class Entity : IEntity
  {
    /// <summary>
    /// Determines if the entity is transient (not persisted).
    /// </summary>
    public virtual bool IsTransient()
    {
      return false;
    }

    /// <summary>
    /// Checks if this entity is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    public override bool Equals(object? obj)
    {
      if (obj == null || obj.GetType() != GetType())
      {
        return false;
      }

      return ReferenceEquals(this, obj);
    }

    /// <summary>
    /// Generates a hash code for this entity.
    /// </summary>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    /// Gets the keys for this entity.
    /// </summary>
    public abstract object?[] GetKeys();
  }


  /// <summary>
  /// Base class for entities with a specified identifier type.
  /// </summary>
  /// <typeparam name="TId">The type of the identifier.</typeparam>
  /// <remarks>
  /// Author: AkbarAmd
  /// </remarks>
  public abstract class Entity<TId> : Entity, IEntity<TId> where TId : IEquatable<TId>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public virtual TId Id { get; set; } = default!;

    /// <summary>
    /// Determines if the entity is transient (not persisted).
    /// </summary>
    public override bool IsTransient()
    {
      return Id.Equals(default(TId));
    }

    /// <summary>
    /// Gets the keys for this entity.
    /// </summary>
    public override object?[] GetKeys()
    {
      return new object?[] { Id };
    }

    /// <summary>
    /// Checks if this entity is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    public override bool Equals(object? obj)
    {
      if (obj == null || obj.GetType() != GetType())
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      var other = (Entity<TId>)obj;

      if (other.IsTransient() || IsTransient())
      {
        return false;
      }

      return Id.Equals(other.Id);
    }

    /// <summary>
    /// Generates a hash code for this entity.
    /// </summary>
    public override int GetHashCode()
    {
      return Id == null ? 0 : Id.GetHashCode();
    }
  }
}
