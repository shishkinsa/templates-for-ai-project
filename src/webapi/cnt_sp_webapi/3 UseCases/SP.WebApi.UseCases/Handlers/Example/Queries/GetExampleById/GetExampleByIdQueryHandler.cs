using Requestum.Contract;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Exceptions;
using SP.WebApi.UseCases.Handlers.Example.Mappings;
using SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById;

/// <summary>
/// Обработчик <see cref="GetExampleByIdQuery"/>.
/// </summary>
public sealed class GetExampleByIdQueryHandler(IExampleItemRepository repository)
    : IAsyncQueryHandler<GetExampleByIdQuery, GetExampleByIdResponse>
{
    public async Task<GetExampleByIdResponse> HandleAsync(
        GetExampleByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var item = await repository.GetByIdAsync(query.Id, cancellationToken);
        if (item is null)
        {
            throw new UseCaseNotFoundException($"Example item '{query.Id}' was not found.");
        }

        return new GetExampleByIdResponse(ExampleMappings.ToDto(item));
    }
}
