using AutoMapper;
using LevelUp.Services.Core.BaseCrudServices.ReadServiceBase;
using LevelUp.Services.Core.CrudRepositoryInterfaces;

namespace LevelUp.Services.Tests.ServiceTests.ReadServiceTests;

public class ReadService<TEntityId, TEntity, TDisplayModel> : ReadServiceBase<IReadRepository<TEntityId, TEntity>, TEntityId, TEntity, TDisplayModel>
where TEntity : class
{
    public ReadService(IReadRepository<TEntityId, TEntity> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}