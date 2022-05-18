using LevelUp.Services.EntityFrameworkCore;
using LevelUp.Services.EntityFrameworkCore.Extensions;
using LevelUp.Services.Tests.TestAssets.Models;
using Microsoft.EntityFrameworkCore;

namespace LevelUp.Services.Tests.TestAssets;

public class TestDbContext : DbContextBase
{
    public TestDbContext(DbContextOptionsBuilder<TestDbContext> options) : base(options.Options)
    {
    }

    public DbSet<DB_Customer> Customers { get; set; }
    public DbSet<DB_Employer> Employers { get; set; }
    public DbSet<DB_Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DB_Address>().HasNoKey();

        modelBuilder.AddSoftDeleteQueryFilter();
    }
}