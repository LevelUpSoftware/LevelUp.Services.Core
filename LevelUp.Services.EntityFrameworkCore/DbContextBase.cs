using LevelUp.Services.Core.BaseEntities;
using LevelUp.Services.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LevelUp.Services.EntityFrameworkCore;

public class DbContextBase : DbContext
{
    public DbContextBase(DbContextOptions options) : base(options)
    {

    }
    public override EntityEntry Remove(object entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (!entity.GetType().GetInterfaces().Contains(typeof(ISoftDeletableEntity))) return base.Remove(entity);

        (entity as ISoftDeletableEntity)!.Deleted = true;

        if (entity.GetType().GetInterfaces().Contains(typeof(IDatabaseEntity<object>)))
        {
            (entity as IDatabaseEntity<object>)!.Updated = DateTimeOffset.UtcNow;
        }

        return Update(entity);

    }

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (!entity.GetType().GetInterfaces().Contains(typeof(ISoftDeletableEntity))) return base.Remove(entity);

        (entity as ISoftDeletableEntity)!.Deleted = true;

        if (entity.GetType().GetInterfaces().Contains(typeof(IDatabaseEntity<object>)))
        {
            (entity as IDatabaseEntity<object>)!.Updated = DateTimeOffset.UtcNow;
        }

        return Update(entity);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddSoftDeleteQueryFilter();
        base.OnModelCreating(modelBuilder);
    }
}