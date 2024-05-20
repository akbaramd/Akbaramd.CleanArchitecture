namespace ACA.Domain.Shared.Core;

/// <summary>
///   Base class for creating value objects in C#.
/// </summary>
/// <remarks>
///   Author: Akbar Ahmadi Saray
/// </remarks>
public abstract class ValueObject
{
    /// <summary>
    ///   Compares this value object with another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///   Generates a hash code for the value object.
    /// </summary>
    /// <returns>A hash code for the value object.</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    ///   Returns a string representation of the value object.
    /// </summary>
    /// <returns>A string representation of the value object.</returns>
    public override string ToString()
    {
        return string.Join(", ", GetEqualityComponents().Select(c => c.ToString()));
    }

    /// <summary>
    ///   NovinChecks if two value objects are equal.
    /// </summary>
    /// <param name="left">The left value object.</param>
    /// <param name="right">The right value object.</param>
    /// <returns>True if the value objects are equal, otherwise false.</returns>
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, right) || left.Equals(right);
    }

    /// <summary>
    ///   NovinChecks if two value objects are not equal.
    /// </summary>
    /// <param name="left">The left value object.</param>
    /// <param name="right">The right value object.</param>
    /// <returns>True if the value objects are not equal, otherwise false.</returns>
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    ///   Retrieves the equality components of the value object.
    /// </summary>
    /// <returns>An enumerable of the equality components.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    ///   NovinChecks if this value object is equal to another value object.
    /// </summary>
    /// <param name="other">The other value object to compare with.</param>
    /// <returns>True if the value objects are equal, otherwise false.</returns>
    public bool Equals(ValueObject? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///   NovinChecks if this value object is equal to another value object.
    /// </summary>
    /// <typeparam name="T">The type of the value objects.</typeparam>
    /// <param name="other">The other value object to compare with.</param>
    /// <returns>True if the value objects are equal, otherwise false.</returns>
    public bool Equals<T>(T? other) where T : ValueObject
    {
        if (other is null)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    ///   NovinChecks if two value objects are equal.
    /// </summary>
    /// <param name="one">The first value object.</param>
    /// <param name="two">The second value object.</param>
    /// <returns>True if the value objects are equal, otherwise false.</returns>
    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    /// <summary>
    ///   NovinChecks if two value objects are not equal.
    /// </summary>
    /// <param name="one">The first value object.</param>
    /// <param name="two">The second value object.</param>
    /// <returns>True if the value objects are not equal, otherwise false.</returns>
    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }
}