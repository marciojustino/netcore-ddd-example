namespace DDDExample.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IEntity<TId> : IEqualityComparer<TId>
    {
        TId Id { get; set; }
    }
}