namespace DDDExample.Domain.Core.Events
{
    using System;
    using Entities;
    using MediatR;

    public class UserLoggedDomainEvent : INotification
    {
        public UserLoggedDomainEvent(User user, DateTime loggedAt)
        {
            User = user;
            LoggedAt = loggedAt;
        }

        public User User { get; }
        public DateTime LoggedAt { get; }
    }
}