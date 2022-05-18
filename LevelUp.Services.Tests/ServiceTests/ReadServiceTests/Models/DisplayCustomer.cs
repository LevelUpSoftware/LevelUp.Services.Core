using System;

namespace LevelUp.Services.Tests.ServiceTests.ReadServiceTests.Models;

public class DisplayCustomer
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}