using FluentValidation;
using Requestum.Contract;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Exceptions;
using SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample.Responses;
using SP.WebApi.UseCases.Handlers.Example.Mappings;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample;

/// <summary>
/// Обработчик команды <see cref="UpdateExampleCommand"/>.
/// </summary>
public sealed class UpdateExampleCommandHandler(
    IExampleItemRepository repository,
    IValidator<UpdateExampleCommand> validator)
    : IAsyncCommandHandler<UpdateExampleCommand, UpdateExampleResponse>
{
    public async Task<UpdateExampleResponse> ExecuteAsync(
        UpdateExampleCommand command,
        CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var item = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (item is null)
        {
            throw new UseCaseNotFoundException($"Example item '{command.Id}' was not found.");
        }

        item.Rename(command.Name);
        await repository.UpdateAsync(item, cancellationToken);
        return new UpdateExampleResponse(ExampleMappings.ToDto(item));
    }
}
