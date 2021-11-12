namespace DDDExample.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using CSharpFunctionalExtensions;
    using Entities;
    using FluentValidation;

    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        TEntity Add<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
        void Delete(Guid id);
        List<TEntity> Get();
        TEntity GetById(Guid id);
        TEntity Update<TValidator>(Guid id, TEntity entity) where TValidator : AbstractValidator<TEntity>;
    }
}