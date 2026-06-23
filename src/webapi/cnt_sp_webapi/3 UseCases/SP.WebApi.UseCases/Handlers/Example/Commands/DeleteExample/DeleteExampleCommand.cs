using Requestum.Contract;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.DeleteExample;

/// <summary>
/// Команда удаления примера сущности.
/// </summary>
public sealed record DeleteExampleCommand(Guid Id) : ICommand;
