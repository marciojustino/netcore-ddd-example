namespace DDDExample.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class Password : ValueObject, IComparable<Password>
    {
        protected Password()
        {
        }

        public Password(string plainTextPassword, string salt) : this()
        {
            Salt = Convert.FromBase64String(salt);
            Value = Encrypt(plainTextPassword, Salt);
        }

        public string Value { get; }

        public byte[] Salt { get; }

        public int CompareTo(Password other) => Value.CompareTo(other.Value);

        public static string Encrypt(string plainTextPassword, byte[] salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(plainTextPassword, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));

        public static string Encrypt(string plainTextPassword, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(plainTextPassword, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));
        }

        public override string ToString() => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}