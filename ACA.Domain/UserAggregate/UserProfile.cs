using System;
using System.Collections.Generic;
using ACA.Domain.Shared.Core;

namespace ACA.Domain.UserAggregate
{
    /// <summary>
    /// Represents a user profile value object.
    /// </summary>
    /// <remarks>
    /// Author: AkbarAmd
    /// </remarks>
    public class UserProfile : ValueObject
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        public UserProfile(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}