using CSharpFunctionalExtensions;
using DDDExample.Domain.Entities;
using DDDExample.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Maybe<User> GetAuthorizedUser(Email email, Password password);
    }
}
