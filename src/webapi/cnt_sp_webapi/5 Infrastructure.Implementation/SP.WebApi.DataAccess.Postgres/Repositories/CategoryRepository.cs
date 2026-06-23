using Microsoft.EntityFrameworkCore;
using SP.WebApi.DataAccess.Postgres.Data;
using SP.WebApi.Entities;
using SP.WebApi.Infrastructure.Interfaces.Repositories;

namespace SP.WebApi.DataAccess.Postgres.Repositories;

/// <summary>
/// Read-only репозиторий справочника категорий.
/// </summary>
public sealed class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }
}
