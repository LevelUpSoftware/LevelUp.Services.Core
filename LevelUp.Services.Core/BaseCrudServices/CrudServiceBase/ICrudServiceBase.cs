using System.Threading.Tasks;
using LevelUp.Services.Core.BaseCrudServices.UpdateServiceBase;

namespace LevelUp.Services.Core.BaseCrudServices.CrudServiceBase;

public interface
    ICrudServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel, TUpdateModel> : IUpdateServiceBase<TEntityId,
        TEntity, TDisplayModel, TCreateModel, TUpdateModel>
    where TEntityId : class, new()
{
    Task DeleteAsync(TEntityId id);
}