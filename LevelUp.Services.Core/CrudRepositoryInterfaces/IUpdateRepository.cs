using System.Threading.Tasks;

namespace LevelUp.Services.Core.CrudRepositoryInterfaces;

public interface IUpdateRepository<TEntityId, TEntity> : ICreateRepository<TEntityId, TEntity>
{
    Task<TEntity> UpdateAsync(TEntity updateEntity, TEntityId id);
}