namespace DDDExample.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Interfaces;
    using FluentValidation;

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository) => _repository = repository;

        public TEntity Add<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            return _repository.Insert(entity);
        }

        public Task DeleteAsync(Guid id) => _repository.DeleteAsync(id);

        public Task<List<TEntity>> GetAsync() => _repository.SelectAsync();

        public Task<TEntity> GetByIdAsync(Guid id) => _repository.SelectAsync(id);

        public async Task<TEntity> Update<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            await _repository.UpdateAsync(entity);
            return entity;
        }

        private void Validate(TEntity entity, AbstractValidator<TEntity> validator)
        {
            if (entity is null)
                throw new Exception("Registros não informados");

            validator.ValidateAndThrow(entity);
        }
    }
}