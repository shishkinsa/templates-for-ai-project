using Requestum.Contract;
using SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample.Responses;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample;

/// <summary>
/// Команда создания примера сущности.
/// </summary>
public sealed record CreateExampleCommand(string Name) : ICommand<CreateExampleResponse>;
