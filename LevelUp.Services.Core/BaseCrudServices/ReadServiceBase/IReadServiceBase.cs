using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelUp.Services.Core.BaseCrudServices.ReadServiceBase
{
    public interface IReadServiceBase<TEntityId, TDisplayModel>
    {
        Task<IEnumerable<TDisplayModel>> GetAllAsync();
        Task<TDisplayModel> GetByIdAsync(TEntityId id);
    }
}