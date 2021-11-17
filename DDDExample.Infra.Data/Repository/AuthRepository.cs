using DDDExample.Domain.Entities;
using DDDExample.Domain.Interfaces.Auth;
using DDDExample.Domain.ValueObjects;
using DDDExample.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Infra.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetAuthorizedUser(Email email, Password password)
        {
            return _dbContext.Set<User>()
                .Where(x => x.Email.Value.Equals(email.Value) && x.CurrentPassword.Value.Equals(password.Value))
                .SingleOrDefault();
        }
    }
}
