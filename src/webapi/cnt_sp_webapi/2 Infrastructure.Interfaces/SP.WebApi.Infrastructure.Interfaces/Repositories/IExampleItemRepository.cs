using SP.WebApi.Entities;

namespace SP.WebApi.Infrastructure.Interfaces.Repositories;

/// <summary>
/// Контракт персистентности для <see cref="ExampleItem"/>.
/// </summary>
public interface IExampleItemRepository
{
    Task AddAsync(ExampleItem item, CancellationToken cancellationToken = default);

    Task<ExampleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ExampleItem>> ListAsync(CancellationToken cancellationToken = default);
}
