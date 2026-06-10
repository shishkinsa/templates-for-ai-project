using Microsoft.EntityFrameworkCore;
using SP.WebApi.Entities;
using SP.WebApi.Infrastructure.Interfaces.DataAccess;

namespace SP.WebApi.DataAccess.Postgres.Data;

/// <summary>
/// EF Core контекст PostgreSQL.
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IDbContext
{
    public DbSet<ExampleItem> ExampleItems => Set<ExampleItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExampleItem>(entity =>
        {
            entity.ToTable("example_items");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(256).IsRequired();
            entity.Property(x => x.CreatedAt).IsRequired();
        });
    }
}
