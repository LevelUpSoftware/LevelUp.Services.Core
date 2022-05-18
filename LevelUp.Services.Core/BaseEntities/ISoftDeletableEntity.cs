namespace LevelUp.Services.Core.BaseEntities;

public interface ISoftDeletableEntity
{
    public bool Deleted { get; set; }
}