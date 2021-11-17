namespace DDDExample.Domain.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using CSharpFunctionalExtensions;
    using Interfaces;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class Password : ValueObject, IComparable<Password>
    {
        public string Value { get; private set; }

        public byte[] Salt { get; private set; }

        protected Password() { }

        public Password(string plainTextPassword, string salt) : this()
        {
            Salt = Convert.FromBase64String(salt);
            Value = Encrypt(plainTextPassword, Salt);
        }

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

        public int CompareTo(Password other) => Value.CompareTo(other.Value);
    }
}