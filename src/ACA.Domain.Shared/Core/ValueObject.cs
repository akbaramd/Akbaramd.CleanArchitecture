using System;
using System.Collections.Generic;
using System.Linq;

namespace ACA.Domain.Shared.Core
{
    /// <summary>
    /// Base class for creating value objects.
    /// </summary>
    /// <remarks>
    /// Author: AkbarAmd
    /// </remarks>
    public abstract class ValueObject
    {
        /// <summary>
        /// Determines whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
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
        /// Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Returns a string representation of the value object.
        /// </summary>
        public override string ToString()
        {
            return string.Join(", ", GetEqualityComponents().Select(c => c?.ToString() ?? ""));
        }

        /// <summary>
        /// Retrieves the equality components of the value object.
        /// </summary>
        protected abstract IEnumerable<object?> GetEqualityComponents();

        /// <summary>
        /// Checks if two value objects are equal.
        /// </summary>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return ReferenceEquals(left, right);
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Checks if two value objects are not equal.
        /// </summary>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        /// <summary>
        /// Determines whether the specified value object is equal to the current value object.
        /// </summary>
        /// <param name="other">The value object to compare with the current value object.</param>
        public bool Equals(ValueObject? other)
        {
            if (other is null)
            {
                return false;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Determines whether the specified value object is equal to the current value object.
        /// </summary>
        /// <typeparam name="T">The type of the value objects.</typeparam>
        /// <param name="other">The value object to compare with the current value object.</param>
        public bool Equals<T>(T? other) where T : ValueObject
        {
            if (other is null)
            {
                return false;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Checks if two value objects are equal.
        /// </summary>
        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return EqualOperator(one, two);
        }

        /// <summary>
        /// Checks if two value objects are not equal.
        /// </summary>
        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return NotEqualOperator(one, two);
        }
    }
}
