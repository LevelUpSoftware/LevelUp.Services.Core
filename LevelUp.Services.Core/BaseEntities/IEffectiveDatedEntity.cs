using System;

namespace LevelUp.Services.Core.BaseEntities;

public interface IEffectiveDatedEntity<TEntityId> : IDatabaseEntity<TEntityId>
{
    DateTimeOffset EffectiveStartTime { get; set; }
    DateTimeOffset EffectiveEndTime { get; set; }
}