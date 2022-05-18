using System;
using LevelUp.Services.Core.BaseEntities;

namespace LevelUp.Services.Tests.TestAssets.Models;

public class DB_Customer : IDatabaseEntity<long>, ISoftDeletableEntity
{
    public long Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public bool Deleted { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}