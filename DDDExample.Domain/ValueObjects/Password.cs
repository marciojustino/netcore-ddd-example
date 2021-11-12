namespace DDDExample.Domain.ValueObjects
{
    using System;
    using CSharpFunctionalExtensions;
    using Interfaces;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class Password : IValueObject<Password>
    {
        public Password(string plainTextPassword, byte[] salt) => CurrentPassword = Encrypt(plainTextPassword, salt);

        public string CurrentPassword { get; private set; }
        public string LastPassword { get; private set; }

        public bool Equals(Password x, Password y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.CurrentPassword == y.CurrentPassword && x.LastPassword == y.LastPassword;
        }

        public int GetHashCode(Password obj) => HashCode.Combine(obj.CurrentPassword, obj.LastPassword);

        public Result ChangePassword(string plainTextPassword, byte[] salt)
        {
            var newPassword = Encrypt(plainTextPassword, salt);
            var canChangePassword = CanChangePassword(newPassword);
            if (canChangePassword.IsFailure)
                return canChangePassword;

            CurrentPassword = newPassword;
            LastPassword = CurrentPassword;
            return Result.Success();
        }

        private string Encrypt(string plainTextPassword, byte[] salt) => 
            Convert.ToBase64String(KeyDerivation.Pbkdf2(plainTextPassword, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));

        private Result CanChangePassword(string newPassword)
        {
            if (newPassword.Equals(LastPassword))
                return Result.Failure("Senha já foi utilizada anteriormente");

            if (newPassword.Equals(CurrentPassword))
                return Result.Failure("Senha não pode ser igual a atual");

            // TODO: Testar complexidade de senha
            return Result.Success();
        }
    }
}