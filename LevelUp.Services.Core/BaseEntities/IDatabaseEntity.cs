using System;

namespace LevelUp.Services.Core.BaseEntities;

public interface IDatabaseEntity<TEntityId>
{
    TEntityId Id { get; set; }
    DateTimeOffset Created { get; set; }
    DateTimeOffset Updated { get; set; }
}