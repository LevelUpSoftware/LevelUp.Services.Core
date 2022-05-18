using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LevelUp.Services.Core.BaseCrudServices.UpdateServiceBase;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.CrudServiceBase;

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

    public virtual async Task DeleteAsync(TEntityId id)
    {
        await Repository.DeleteAsync(id);
    }
}