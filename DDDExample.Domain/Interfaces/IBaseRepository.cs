namespace DDDExample.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Insert(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<List<TEntity>> SelectAsync();
        Task<TEntity> SelectAsync(Guid id);
    }
}