namespace DDDExample.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using MediatR;

    public abstract class BaseEntity : IEntity<Guid>
    {
        private List<INotification> _domainEvents;

        public bool Equals(Guid x, Guid y) => x.Equals(y);

        public int GetHashCode(Guid obj) => obj.GetHashCode();

        public Guid Id { get; set; }

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(INotification @event) => _domainEvents.Remove(@event);
    }
}