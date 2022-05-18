using System.Threading.Tasks;
using LevelUp.Services.Core.BaseCrudServices.ReadServiceBase;
using LevelUp.Services.Core.FluentValidation;

namespace LevelUp.Services.Core.BaseCrudServices.CreateServiceBase;

public interface ICreateServiceBase<TEntityId, TEntity, TDisplayModel, TCreateModel> 
    : IReadServiceBase<TEntityId, TDisplayModel>
{
    Task<ServiceActionResult<TDisplayModel>> CreateAsync(TCreateModel createItem);
}