using AutoMapper;
using FluentValidation;
using LevelUp.Services.Core.BaseCrudServices.CreateServiceBase;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using System.Linq;
using System.Threading.Tasks;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.UpdateServiceBase;

/// <summary>
/// CRUD services. Update service base.
/// </summary>
/// <typeparam name="TRepository">Data repository type.</typeparam>
/// <typeparam name="TEntityId">Database entity id type.</typeparam>
/// <typeparam name="TEntity">Database entity type.</typeparam>
/// <typeparam name="TDisplayModel">Display model DTO type.</typeparam>
/// <typeparam name="TCreateModel">Create model DTO type.</typeparam>
/// <typeparam name="TCreateValidator">Create model fluent validator type.</typeparam>
/// <typeparam name="TUpdateModel">Update model DTO type.</typeparam>
/// <typeparam name="TUpdateValidator">Update model fluent validator type.</typeparam>
public abstract class UpdateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator,
        TUpdateModel, TUpdateValidator>
    : CreateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator>,
        IUpdateServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel, TUpdateModel>
    where TRepository : IUpdateRepository<TEntityId, TEntity>
    where TEntity : class
    where TUpdateValidator : UpdateValidatorBase<TUpdateModel, TEntityId>
    where TCreateValidator : AbstractValidator<TCreateModel>
{
    protected TUpdateValidator UpdateValidator { get; }

    protected UpdateServiceBase(
        TRepository repository,
        IMapper mapper,
        TCreateValidator createValidator,
        TUpdateValidator updateValidator) 
        : base(repository, mapper, createValidator)
    {
        UpdateValidator = updateValidator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateItem"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ServiceActionResult<TDisplayModel>> UpdateAsync(TUpdateModel updateItem, TEntityId id)
    {
        UpdateValidator.Id = id;
        var validationResults = await UpdateValidator.ValidateAsync(updateItem);
        if (!validationResults.IsValid)
        {
            return new ServiceActionResult<TDisplayModel>(validationResults.Errors.Select(x => x.ErrorMessage));
        }

        TEntity? dbEntity = await Repository.GetByIdAsync(id);

        if (dbEntity == null)
        {
            return new ServiceActionResult<TDisplayModel>($"An entity with an id of {id} was not found.");
        }

        Mapper.Map(updateItem, dbEntity);

        var updateResults = await Repository.UpdateAsync(dbEntity, id);

        var mappedResult = Mapper.Map<TDisplayModel>(updateResults);
        return new ServiceActionResult<TDisplayModel>(mappedResult);
    }

    /// <summary>
    /// Returns an instance of a mapped update DTO. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TUpdateModel> GetUpdateModelAsync(TEntityId id)
    {
        var dbResult = await Repository.GetByIdAsync(id);

        var mappedResult = Mapper.Map<TUpdateModel>(dbResult);

        return mappedResult;
    }
}