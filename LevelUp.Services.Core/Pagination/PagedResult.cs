using System.Collections.Generic;

namespace LevelUp.Services.Core.Pagination;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int currentPage, int pageSize, int totalRecords)
    {
        ResultItems = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
    public IEnumerable<T> ResultItems { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }
}