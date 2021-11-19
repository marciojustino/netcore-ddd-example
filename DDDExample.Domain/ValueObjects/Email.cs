namespace DDDExample.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;

    public class Email : ValueObject, IComparable<Email>
    {
        protected Email()
        {
        }

        public Email(string email) : this() => Value = email;

        public string Value { get; protected set; }

        public int CompareTo(Email other) => Value.CompareTo(other.Value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}