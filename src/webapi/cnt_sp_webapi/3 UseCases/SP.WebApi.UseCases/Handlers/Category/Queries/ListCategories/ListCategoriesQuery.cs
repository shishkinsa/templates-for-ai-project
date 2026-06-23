using Requestum.Contract;
using SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories.Responses;

namespace SP.WebApi.UseCases.Handlers.Category.Queries.ListCategories;

/// <summary>
/// Запрос списка категорий.
/// </summary>
public sealed record ListCategoriesQuery : IQuery<ListCategoriesResponse>;
