using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LevelUp.Services.Core.BaseCrudServices.UpdateServiceBase;
using LevelUp.Services.Core.BaseEntities;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.CrudServiceBase;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRepository">Data repository type.</typeparam>
/// <typeparam name="TEntityId">Database entity id type.</typeparam>
/// <typeparam name="TEntity">Database entity type.</typeparam>
/// <typeparam name="TDisplayModel">Display model DTO type.</typeparam>
/// <typeparam name="TCreateModel">Create model DTO type.</typeparam>
/// <typeparam name="TCreateValidator">Create model fluent validator type.</typeparam>
/// <typeparam name="TUpdateModel">Update model DTO type.</typeparam>
/// <typeparam name="TUpdateValidator">Update model fluent validator type.</typeparam>
public class CrudServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator, TUpdateModel, TUpdateValidator> 
    : UpdateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator, TUpdateModel, TUpdateValidator>,
        ICrudServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel, TUpdateModel>
where TRepository : ICrudRepository<TEntityId, TEntity>
where TEntity : class, new()
where TEntityId : class, new()
where TCreateValidator : AbstractValidator<TCreateModel>
where TUpdateValidator : UpdateValidatorBase<TUpdateModel, TEntityId>
{

    public CrudServiceBase(
        TRepository repository,
        IMapper mapper,
        TCreateValidator createValidator,
        TUpdateValidator updateValidator)
    : base(repository, mapper, createValidator, updateValidator)
    {
    }

    /// <summary>
    /// Deletes entity with specified Id. Automatically soft delete <code>ISoftDeletable</code> entity.
    /// </summary>
    /// <param name="id">Id of the entity to delete.</param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(TEntityId id)
    {
        if (typeof(TEntity) is ISoftDeletableEntity)
        {
            var dbEntity = await Repository.GetByIdAsync(id);
            if (dbEntity == null) return;

            ((ISoftDeletableEntity)dbEntity).Deleted = true;

            if (typeof(TEntity) is IDatabaseEntity<TEntityId>)
            {
                ((IDatabaseEntity<TEntityId>)dbEntity).Updated = DateTimeOffset.UtcNow;
            }

            await Repository.UpdateAsync(dbEntity, id);
            return;
        }
        await Repository.DeleteAsync(id);
    }
}