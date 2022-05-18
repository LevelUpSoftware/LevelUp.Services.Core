using System.Threading.Tasks;

namespace LevelUp.Services.Core.CrudRepositoryInterfaces;

public interface ICrudRepository<TEntityId, TEntity> : IUpdateRepository<TEntityId, TEntity>
{
    Task DeleteAsync(TEntityId id);
}