using System.ComponentModel.DataAnnotations;

namespace LevelUp.Services.Core.Pagination;

public enum SortDirection
{
    [Display(Name = "ASCENDING")]
    Ascending,
    [Display(Name = "DESCENDING")]
    Descending
}