using CSharpFunctionalExtensions;
using DDDExample.Domain.Dtos;
using DDDExample.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<User>> LoginAsync(AuthDto auth);
    }
}
