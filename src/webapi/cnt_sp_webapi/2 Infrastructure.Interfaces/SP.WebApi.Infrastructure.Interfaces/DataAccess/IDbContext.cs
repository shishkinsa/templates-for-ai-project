using Microsoft.EntityFrameworkCore;

namespace SP.WebApi.Infrastructure.Interfaces.DataAccess;

/// <summary>
/// Абстракция контекста данных для UseCases.
/// </summary>
public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
