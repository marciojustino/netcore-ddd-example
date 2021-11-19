namespace DDDExample.Domain.Interfaces.Auth
{
    using CSharpFunctionalExtensions;
    using Entities;
    using ValueObjects;

    public interface IAuthRepository
    {
        Maybe<User> GetAuthorizedUser(Email email, Password password);
    }
}