using System.Threading.Tasks;

namespace LevelUp.Services.Core.CrudRepositoryInterfaces;

public interface ICreateRepository<TEntityId, TEntity> : IReadRepository<TEntityId, TEntity>
{
    Task<TEntity> CreateAsync(TEntity createEntity);
}