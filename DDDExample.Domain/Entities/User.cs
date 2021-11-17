namespace DDDExample.Domain.Entities
{
    using System;
    using ValueObjects;

    public class User : BaseEntity
    {
        public string Name { get; set; }
        public virtual Email Email { get; set; }
        public virtual Password CurrentPassword { get; private set; }
        public virtual Password LastPassword { get; private set; }

        protected User() { }

        public User(string name, string email, string password, string salt) : this()
        {
            Name = name;
            Email = new Email(email);
            LastPassword = CurrentPassword;
            CurrentPassword = new Password(password, salt);
        }

        public void ChangePassword(string newPlainTextPassword, string salt)
        {
            var newPassword = Password.Encrypt(newPlainTextPassword, salt);
            if (!IsValidPassword(newPassword))
                throw new InvalidOperationException("New password are not in pattern!");

            LastPassword = CurrentPassword;
            CurrentPassword = new Password(newPlainTextPassword, salt);
        }

        private bool IsValidPassword(string newPassword) => !newPassword.Equals(CurrentPassword) && !newPassword.Equals(LastPassword);
    }
}