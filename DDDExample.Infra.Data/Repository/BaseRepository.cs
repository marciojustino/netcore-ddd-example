namespace DDDExample.Infra.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Context;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public TEntity Insert(TEntity entity)
        {
            var newEntity = _dbContext.Set<TEntity>().Add(entity).Entity;
            _dbContext.SaveChanges();
            return newEntity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _dbContext.Set<TEntity>().Remove(await SelectAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<TEntity>> SelectAsync() => _dbContext.Set<TEntity>().ToListAsync();

        public Task<TEntity> SelectAsync(Guid id) => _dbContext.Set<TEntity>().FindAsync(id).AsTask();
    }
}