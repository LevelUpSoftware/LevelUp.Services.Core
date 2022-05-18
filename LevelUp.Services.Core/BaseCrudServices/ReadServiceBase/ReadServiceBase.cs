using System;
using AutoMapper;
using LevelUp.Services.Core.CrudRepositoryInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using LevelUp.Services.Core.Pagination;

namespace LevelUp.Services.Core.BaseCrudServices.ReadServiceBase;

public abstract class
    ReadServiceBase<TRepository, TEntityId, TEntity, TDisplayModel> : IReadServiceBase<TEntityId, TDisplayModel>, IPageableService<TEntity, TEntityId, TDisplayModel>
    where TRepository : IReadRepository<TEntityId, TEntity>
    where TEntity : class
{
    protected ReadServiceBase(TRepository repository, IMapper mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    protected IMapper Mapper { get; }
    protected TRepository Repository { get; }

    /// <summary>
    /// Queries the repository for an entity with the specified <paramref name="id"/> and returns a mapped DTO asynchronously.
    /// </summary>
    /// <param name="id">Entity Id field value.</param>
    /// <returns></returns>
    public virtual async Task<TDisplayModel> GetByIdAsync(TEntityId id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id), "Argument 'id' must not be null.");
        }

        TEntity? dbEntity = await Repository.GetByIdAsync(id);

        var mappedResult = Mapper.Map<TDisplayModel>(dbEntity);
        return mappedResult;
    }

    /// <summary>
    /// Queries the repository and returns an IEnumerable of mapped DTO's
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TDisplayModel>> GetAllAsync()
    {
        var dbEntities = await Repository.GetAllAsync();

        var mappedResults = Mapper.Map<List<TDisplayModel>>(dbEntities);
        return mappedResults;
    }

    /// <summary>
    /// Queries the repository and returns paged results.
    /// </summary>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="pagedQueryOptions"></param>
    /// <returns></returns>
    public virtual async Task<PagedResult<TDisplayModel>> GetPagedAsync(int currentPage, int pageSize,
        PagedQueryOptions<TEntity, TEntityId>? pagedQueryOptions = null)
    {
        var queryOptions = pagedQueryOptions ?? new PagedQueryOptions<TEntity, TEntityId>();
        if (currentPage < 0) currentPage = 0;

        var dbResults = await Repository.GetPagedAsync(currentPage, pageSize, pagedQueryOptions);

        var mappedResults = Mapper.Map<IEnumerable<TDisplayModel>>(dbResults.ResultItems);

        var pagedResult = new PagedResult<TDisplayModel>(mappedResults, dbResults.CurrentPage, dbResults.PageSize,
            dbResults.TotalRecords);

        return pagedResult;
    }
}