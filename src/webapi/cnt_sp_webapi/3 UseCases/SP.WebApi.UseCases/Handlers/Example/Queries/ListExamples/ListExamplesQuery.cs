using Requestum.Contract;
using SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples;

/// <summary>
/// Запрос списка примеров сущностей.
/// </summary>
public sealed record ListExamplesQuery : IQuery<ListExamplesResponse>;
