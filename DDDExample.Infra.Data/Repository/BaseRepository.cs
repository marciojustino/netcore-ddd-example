namespace DDDExample.Infra.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Context;
    using Domain.Entities;
    using Domain.Interfaces;

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public TEntity Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            _dbContext.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity).Entity;

        public void Delete(Guid id) => _dbContext.Set<TEntity>().Remove(Select(id));

        public List<TEntity> Select() => _dbContext.Set<TEntity>().ToList();

        public TEntity Select(Guid id) => _dbContext.Set<TEntity>().Find(id);
    }
}