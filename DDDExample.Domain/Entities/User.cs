namespace DDDExample.Domain.Entities
{
    using ValueObjects;

    public class User : BaseEntity
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public User(string name, string email, string password, byte[] salt)
            : this(name, email) => Password = new Password(password, salt);

        public string Name { get; }
        public string Email { get; }
        public Password Password { get; }

        public void ChangePassword(string plainTextPassword, byte[] salt) => Password.ChangePassword(plainTextPassword, salt);
    }
}