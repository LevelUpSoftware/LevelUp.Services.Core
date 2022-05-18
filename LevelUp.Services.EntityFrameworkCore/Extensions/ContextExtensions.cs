using System.Linq.Expressions;
using LevelUp.Services.Core.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LevelUp.Services.EntityFrameworkCore.Extensions;

public static class ContextExtensions
{
    public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetInterface(typeof(TInterface).Name) != null)
            {
                var newParam = Expression.Parameter(entityType.ClrType);
                var newBody = ReplacingExpressionVisitor
                    .Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(Expression.Lambda(newBody, newParam));
            }
        }
    }

    public static void AddSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyGlobalFilters<ISoftDeletableEntity>(x => !x.Deleted);
    }
}