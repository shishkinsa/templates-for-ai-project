using Requestum.Contract;
using SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample;

/// <summary>
/// Команда обновления примера сущности.
/// </summary>
public sealed record UpdateExampleCommand(Guid Id, string Name) : ICommand<UpdateExampleResponse>;
