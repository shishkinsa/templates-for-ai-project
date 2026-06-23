using Microsoft.AspNetCore.Mvc;
using Requestum;
using SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories;
using SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories.Responses;

namespace SP.WebApi.WebApp.Controllers;

/// <summary>
/// REST API справочника категорий (read-only).
/// </summary>
[ApiController]
[Route("api/v1/categories")]
public sealed class CategoriesController(IRequestum requestum) : ControllerBase
{
    /// <summary>
    /// Возвращает список категорий.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ListCategoriesResponse), StatusCodes.Status200OK)]
    public Task<ListCategoriesResponse> List(CancellationToken cancellationToken) =>
        requestum.HandleAsync<ListCategoriesQuery, ListCategoriesResponse>(
            new ListCategoriesQuery(),
            cancellationToken);
}
