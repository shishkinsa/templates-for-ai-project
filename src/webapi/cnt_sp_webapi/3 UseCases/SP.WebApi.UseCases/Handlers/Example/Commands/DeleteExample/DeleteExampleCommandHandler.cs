using Requestum.Contract;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Exceptions;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.DeleteExample;

/// <summary>
/// Обработчик команды <see cref="DeleteExampleCommand"/>.
/// </summary>
public sealed class DeleteExampleCommandHandler(IExampleItemRepository repository)
    : IAsyncCommandHandler<DeleteExampleCommand>
{
    public async Task ExecuteAsync(DeleteExampleCommand command, CancellationToken cancellationToken = default)
    {
        var item = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (item is null)
        {
            throw new UseCaseNotFoundException($"Example item '{command.Id}' was not found.");
        }

        await repository.DeleteAsync(command.Id, cancellationToken);
    }
}
