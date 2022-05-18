using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LevelUp.Services.Core.BaseCrudServices.ReadServiceBase;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.CreateServiceBase;

public abstract class CreateServiceBase<TRepository, TEntityId, TEntity, TDisplayModel, TCreateModel, TCreateValidator> 
    : ReadServiceBase<TRepository, TEntityId, TEntity, TDisplayModel>, 
        ICreateServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel>
    where TRepository : ICreateRepository<TEntityId, TEntity>
    where TEntity : class
    where TCreateValidator : AbstractValidator<TCreateModel>
{
    protected TCreateValidator CreateValidator { get; }

    protected CreateServiceBase(
        TRepository repository,
        IMapper mapper,
        TCreateValidator createValidator)
    : base(repository, mapper)
    {
        CreateValidator = createValidator;
    }
    /// <summary>
    /// Validate <paramref name="createItem"/>, create record in repository, and return mapped display DTO.
    /// </summary>
    /// <param name="createItem">DTO instance to create.</param>
    /// <returns></returns>
    public virtual async Task<ServiceActionResult<TDisplayModel>> CreateAsync(TCreateModel createItem)
    {
        var validationResults = await CreateValidator.ValidateAsync(createItem);
        if (!validationResults.IsValid)
        {
            return new ServiceActionResult<TDisplayModel>(validationResults.Errors.Select(x => x.ErrorMessage));
        }

        var mappedDbCreateItem = Mapper.Map<TEntity>(createItem);

        var createResult = await Repository.CreateAsync(mappedDbCreateItem);

        var mappedResult = Mapper.Map<TDisplayModel>(createResult);
        return new ServiceActionResult<TDisplayModel>(mappedResult);
    }
}