using Microsoft.AspNetCore.Mvc;
using Requestum;
using SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample;
using SP.WebApi.UseCases.Handlers.Example.Commands.CreateExample.Responses;
using SP.WebApi.UseCases.Handlers.Example.Commands.DeleteExample;
using SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample;
using SP.WebApi.UseCases.Handlers.Example.Commands.UpdateExample.Responses;
using SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById;
using SP.WebApi.UseCases.Handlers.Example.Queries.GetExampleById.Responses;
using SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples;
using SP.WebApi.UseCases.Handlers.Example.Queries.ListExamples.Responses;

namespace SP.WebApi.WebApp.Controllers;

/// <summary>
/// REST API для эталонной сущности ExampleItem.
/// </summary>
[ApiController]
[Route("api/v1/examples")]
public sealed class ExamplesController(IRequestum requestum) : ControllerBase
{
    /// <summary>
    /// Возвращает список примеров.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ListExamplesResponse), StatusCodes.Status200OK)]
    public Task<ListExamplesResponse> List(CancellationToken cancellationToken) =>
        requestum.HandleAsync<ListExamplesQuery, ListExamplesResponse>(
            new ListExamplesQuery(),
            cancellationToken);

    /// <summary>
    /// Возвращает пример по идентификатору.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetExampleByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<GetExampleByIdResponse> GetById(Guid id, CancellationToken cancellationToken) =>
        requestum.HandleAsync<GetExampleByIdQuery, GetExampleByIdResponse>(
            new GetExampleByIdQuery(id),
            cancellationToken);

    /// <summary>
    /// Создаёт новый пример.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateExampleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateExampleResponse>> Create(
        [FromBody] CreateExampleRequest request,
        CancellationToken cancellationToken)
    {
        var response = await requestum.ExecuteAsync<CreateExampleCommand, CreateExampleResponse>(
            new CreateExampleCommand(request.Name));

        return CreatedAtAction(nameof(GetById), new { id = response.Item.Id }, response);
    }

    /// <summary>
    /// Обновляет пример по идентификатору.
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateExampleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<UpdateExampleResponse> Update(
        Guid id,
        [FromBody] UpdateExampleRequest request,
        CancellationToken cancellationToken) =>
        requestum.ExecuteAsync<UpdateExampleCommand, UpdateExampleResponse>(
            new UpdateExampleCommand(id, request.Name));

    /// <summary>
    /// Удаляет пример по идентификатору.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await requestum.ExecuteAsync(new DeleteExampleCommand(id));
        return NoContent();
    }
}

/// <summary>
/// Тело запроса создания примера.
/// </summary>
public sealed record CreateExampleRequest(string Name);

/// <summary>
/// Тело запроса обновления примера.
/// </summary>
public sealed record UpdateExampleRequest(string Name);
