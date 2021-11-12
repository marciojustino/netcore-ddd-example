namespace DDDExample.Service
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CSharpFunctionalExtensions;
    using Domain.Entities;
    using Domain.Interfaces;
    using FluentValidation;
    using Infra.Data.Context;

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public TEntity Add<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            var newEntity = _repository.Insert(entity);
            _dbContext.SaveChanges();
            return newEntity;
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
            _dbContext.SaveChanges();
        }

        public List<TEntity> Get() => _repository.Select();

        public TEntity GetById(Guid id) => _repository.Select(id);

        public TEntity Update<TValidator>(Guid id, TEntity entity) where TValidator : AbstractValidator<TEntity>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            entity.Id = id;
            var updatedEntity = _repository.Update(entity);
            _dbContext.SaveChanges();
            return updatedEntity;
        }

        private void Validate(TEntity entity, AbstractValidator<TEntity> validator)
        {
            if (entity is null)
                throw new Exception("Registros não informados");

            validator.ValidateAndThrow(entity);
        }
    }
}