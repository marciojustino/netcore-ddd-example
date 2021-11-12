namespace DDDExample.Domain.Entities
{
    using System;
    using Interfaces;

    public abstract class BaseEntity : IEntity<Guid>
    {
        public bool Equals(Guid x, Guid y) => x.Equals(y);

        public int GetHashCode(Guid obj) => obj.GetHashCode();

        public Guid Id { get; set; }
    }
}