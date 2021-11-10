namespace DDDExample.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using FluentValidation;

    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
        Task DeleteAsync(Guid id);
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> Update<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
    }
}