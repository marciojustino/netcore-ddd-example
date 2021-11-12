namespace DDDExample.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IValueObject<in T> : IEqualityComparer<T>
    {
    }
}