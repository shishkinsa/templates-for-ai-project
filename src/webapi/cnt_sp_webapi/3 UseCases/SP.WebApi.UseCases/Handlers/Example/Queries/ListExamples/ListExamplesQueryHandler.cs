using Requestum.Contract;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Handlers.Example.Mappings;
using SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples;

/// <summary>
/// Обработчик <see cref="ListExamplesQuery"/>.
/// </summary>
public sealed class ListExamplesQueryHandler(IExampleItemRepository repository)
    : IAsyncQueryHandler<ListExamplesQuery, ListExamplesResponse>
{
    public async Task<ListExamplesResponse> HandleAsync(
        ListExamplesQuery query,
        CancellationToken cancellationToken = default)
    {
        var items = await repository.ListAsync(cancellationToken);
        var dtos = items.Select(ExampleMappings.ToDto).ToList();
        return new ListExamplesResponse(dtos);
    }
}
