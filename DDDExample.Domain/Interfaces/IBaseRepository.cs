namespace DDDExample.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(Guid id);
        List<TEntity> Select();
        TEntity Select(Guid id);
    }
}