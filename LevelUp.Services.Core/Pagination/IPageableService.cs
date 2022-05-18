using System.Threading.Tasks;

namespace LevelUp.Services.Core.Pagination;

public interface IPageableService<TEntity, TEntityId, TReturns>
{
    Task<PagedResult<TReturns>> GetPagedAsync(int pageNumber, int pageSize, PagedQueryOptions<TEntity, TEntityId> queryOptions);
}