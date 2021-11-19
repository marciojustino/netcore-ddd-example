using CSharpFunctionalExtensions;
using DDDExample.Domain.Core.MessageBus;
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
        private readonly IMessageBusPublisher<string> _messageBus;

        public AuthService(IAuthRepository authRepository, IMessageBusPublisher<string> messageBus)
        {
            _authRepository = authRepository;
            _messageBus = messageBus;
        }

        public Task<Result<User>> LoginAsync(AuthDto auth)
        {
            Validate(auth, new AuthValidator());
            var userResult = _authRepository.GetAuthorizedUser(new Email(auth.Email), new Password(auth.Password, auth.Salt));
            if (!userResult.HasValue)
            {
                _messageBus.PublishAsync(auth.Email);
                return Task.FromResult(Result.Failure<User>("User unauthorized"));
            }

            return Task.FromResult(Result.Success(userResult.Value));
        }

        private void Validate(AuthDto auth, AuthValidator validator)
        {
            if (auth is null) throw new ArgumentNullException(nameof(auth));
            validator.ValidateAndThrow(auth);
        }
    }
}
