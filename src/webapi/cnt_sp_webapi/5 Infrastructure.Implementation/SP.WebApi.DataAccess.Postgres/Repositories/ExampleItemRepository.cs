using Microsoft.EntityFrameworkCore;
using SP.WebApi.DataAccess.Postgres.Data;
using SP.WebApi.Entities;
using SP.WebApi.Infrastructure.Interfaces.Repositories;

namespace SP.WebApi.DataAccess.Postgres.Repositories;

/// <summary>
/// Реализация репозитория <see cref="ExampleItem"/> через EF Core.
/// </summary>
public sealed class ExampleItemRepository(AppDbContext dbContext) : IExampleItemRepository
{
    public async Task AddAsync(ExampleItem item, CancellationToken cancellationToken = default)
    {
        dbContext.ExampleItems.Add(item);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<ExampleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.ExampleItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ExampleItem>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.ExampleItems
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
