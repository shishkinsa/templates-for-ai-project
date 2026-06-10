using FluentValidation;
using Requestum.Contract;
using SP.WebApi.Entities;
using SP.WebApi.Infrastructure.Interfaces.Repositories;
using SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample.Responses;
using SP.WebApi.UseCases.Handlers.Example.Mappings;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample;

/// <summary>
/// Обработчик команды <see cref="CreateExampleCommand"/>.
/// </summary>
public sealed class CreateExampleCommandHandler(
    IExampleItemRepository repository,
    IValidator<CreateExampleCommand> validator)
    : IAsyncCommandHandler<CreateExampleCommand, CreateExampleResponse>
{
    public async Task<CreateExampleResponse> ExecuteAsync(
        CreateExampleCommand command,
        CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var item = ExampleItem.Create(command.Name);
        await repository.AddAsync(item, cancellationToken);
        return new CreateExampleResponse(ExampleMappings.ToDto(item));
    }
}
