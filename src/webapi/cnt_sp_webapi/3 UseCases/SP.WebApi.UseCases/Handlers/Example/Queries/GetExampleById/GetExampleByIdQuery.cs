using Requestum.Contract;
using SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById;

/// <summary>
/// Запрос примера сущности по идентификатору.
/// </summary>
public sealed record GetExampleByIdQuery(Guid Id) : IQuery<GetExampleByIdResponse>;
