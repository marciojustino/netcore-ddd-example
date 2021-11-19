namespace DDDExample.Infra.Data.Repository
{
    using System.Linq;
    using Context;
    using CSharpFunctionalExtensions;
    using Domain.Entities;
    using Domain.Interfaces.Auth;
    using Domain.ValueObjects;

    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public Maybe<User> GetAuthorizedUser(Email email, Password password)
        {
            var user = _dbContext.Set<User>()
                .Where(x => x.Email.Value.Equals(email.Value) && x.CurrentPassword.Value.Equals(password.Value))
                .SingleOrDefault();

            if (user is null)
                return Maybe<User>.None;

            return Maybe.From(user);
        }
    }
}