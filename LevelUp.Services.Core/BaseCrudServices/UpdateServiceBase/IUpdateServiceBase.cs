using System.Threading.Tasks;
using LevelUp.Services.Core.BaseCrudServices.CreateServiceBase;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.UpdateServiceBase;

public interface IUpdateServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel, TUpdateModel>
    : ICreateServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel>
{
    Task<ServiceActionResult<TDisplayModel>> UpdateAsync(TUpdateModel updateItem, TEntityId id);
    Task<TUpdateModel> GetUpdateModelAsync(TEntityId id);
}
