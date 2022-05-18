using System.Collections.Generic;
using System.Threading.Tasks;
using LevelUp.Services.Core.Pagination;

namespace LevelUp.Services.Core.CrudRepositoryInterfaces;

public interface IReadRepository<TEntityId, TEntity>
{
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<PagedResult<TEntity>> GetPagedAsync(int currentPage, int pageSize, PagedQueryOptions<TEntity, TEntityId>? queryOptions = null);
}