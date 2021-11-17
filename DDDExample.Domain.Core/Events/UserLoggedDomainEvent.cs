using DDDExample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Core.Events
{
    public class UserLoggedDomainEvent : INotification
    {
        public User User { get; private set; }
        public DateTime LoggedAt { get; private set; }

        public UserLoggedDomainEvent(User user, DateTime loggedAt)
        {
            User = user;
            LoggedAt= loggedAt;
        }
    }
}
