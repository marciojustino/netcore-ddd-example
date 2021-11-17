using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.ValueObjects
{
    public class Email : ValueObject, IComparable<Email>
    {
        public string Value { get; protected set; }

        protected Email() { }

        public Email(string email) : this()
        {
            Value = email;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public int CompareTo(Email other) => Value.CompareTo(other.Value);
    }
}
