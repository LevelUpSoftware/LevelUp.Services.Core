using System;
using System.Linq.Expressions;

namespace LevelUp.Services.Core.Pagination;

public class PagedQueryOptions<TEntity, TKey>
{
    /// <summary>
    /// Direction to sort query results. Default is Ascending.
    /// </summary>
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    
    /// <summary>
    /// Query filter expression. Ex: x => x.Id == 1234
    /// </summary>
    public Expression<Func<TEntity, bool>>? Filter { get; set; } = null;

    /// <summary>
    /// The entity property to sort by.
    /// Example: <c>x => x.Name</c>
    /// </summary>
    public Func<TEntity, TKey>? SortBy { get; set; } = null;
}