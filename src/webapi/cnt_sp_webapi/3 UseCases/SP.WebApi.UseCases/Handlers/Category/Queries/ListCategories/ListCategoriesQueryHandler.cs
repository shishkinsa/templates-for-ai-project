using Requestum.Contract;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Handlers.Category.Mappings;
using SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories.Responses;

namespace SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories;

/// <summary>
/// Обработчик <see cref="ListCategoriesQuery"/>.
/// </summary>
public sealed class ListCategoriesQueryHandler(ICategoryRepository repository)
    : IAsyncQueryHandler<ListCategoriesQuery, ListCategoriesResponse>
{
    public async Task<ListCategoriesResponse> HandleAsync(
        ListCategoriesQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await repository.ListAsync(cancellationToken);
        var dtos = items.Select(CategoryMappings.ToDto).ToList();
        return new ListCategoriesResponse(dtos);
    }
}
