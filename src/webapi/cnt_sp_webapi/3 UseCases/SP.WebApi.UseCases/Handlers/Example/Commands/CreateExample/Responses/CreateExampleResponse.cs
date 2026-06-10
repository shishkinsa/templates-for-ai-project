using SP.WebApi.UseCases.Handlers.Example.Dto;

namespace SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample.Responses;

/// <summary>
/// Ответ сценария создания примера.
/// </summary>
public sealed record CreateExampleResponse(ExampleItemDto Item);
