using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LevelUp.Services.Core.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum sortDirection)
    {
        return sortDirection.GetType()
            .GetMember(sortDirection.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()!
            .Name!;
    }
}