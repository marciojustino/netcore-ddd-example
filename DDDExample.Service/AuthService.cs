namespace DDDExample.Service
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain.Core.MessageBus;
    using Domain.Dtos;
    using Domain.Entities;
    using Domain.Interfaces.Auth;
    using Domain.ValueObjects;
    using FluentValidation;
    using Validators;

    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMessageBusPublisher<string> _messageBus;
        private readonly ISubscriberOptions _options;

        public AuthService(IAuthRepository authRepository, IMessageBusPublisher<string> messageBus, ISubscriberOptions options)
        {
            _authRepository = authRepository;
            _messageBus = messageBus;
            _options = options;
        }

        public Task<Result<User>> LoginAsync(AuthDto auth)
        {
            Validate(auth, new AuthValidator());
            var userResult = _authRepository.GetAuthorizedUser(new Email(auth.Email), new Password(auth.Password, auth.Salt));
            if (!userResult.HasValue)
            {
                _messageBus.PublishAsync(_options.ExchangeName, auth.Email);
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