using CSharpFunctionalExtensions;
using DDDExample.Domain.Dtos;
using DDDExample.Domain.Entities;
using DDDExample.Domain.Interfaces;
using DDDExample.Domain.Interfaces.Auth;
using DDDExample.Domain.ValueObjects;
using DDDExample.Service.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository) => _authRepository = authRepository;

        public Task<Result<User>> LoginAsync(AuthDto auth)
        {
            Validate(auth, new AuthValidator());
            var authorizedUser = _authRepository.GetAuthorizedUser(new Email(auth.Email), new Password(auth.Password, auth.Salt));
            if (authorizedUser is null)
                return Task.FromResult(Result.Failure<User>("User unauthorized"));

            return Task.FromResult(Result.Success(authorizedUser));
        }

        private void Validate(AuthDto auth, AuthValidator validator)
        {
            if (auth is null) throw new ArgumentNullException(nameof(auth));
            validator.ValidateAndThrow(auth);
        }
    }
}
