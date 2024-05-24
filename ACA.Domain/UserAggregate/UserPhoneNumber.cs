using ACA.Domain.Shared.Core;
using System.Text.RegularExpressions;

namespace ACA.Domain.UserAggregate
{
    /// <summary>
    /// Represents a user phone number value object.
    /// </summary>
    /// <remarks>
    /// Author: AkbarAmd
    /// </remarks>
    public class UserPhoneNumber : ValueObject
    {
        private static readonly Regex CodeRegex = new Regex(@"^\+\d{1,3}$");
        private static readonly Regex NumberRegex = new Regex(@"^\d{10,15}$");

        public UserPhoneNumber(string? code, string? number)
        {
            if (code == null || !CodeRegex.IsMatch(code))
            {
                throw new ArgumentException("Invalid phone code format", nameof(code));
            }

            if (number == null || !NumberRegex.IsMatch(number))
            {
                throw new ArgumentException("Invalid phone number format", nameof(number));
            }

            Code = code;
            Number = number;
        }

        public string? Code { get; }
        public string? Number { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Code;
            yield return Number;
        }

        public override string ToString()
        {
            return $"{Code} {Number}";
        }
    }
}
