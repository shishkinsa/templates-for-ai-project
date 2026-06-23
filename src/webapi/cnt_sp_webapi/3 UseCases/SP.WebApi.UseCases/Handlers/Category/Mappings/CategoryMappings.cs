using CategoryEntity = SP.WebApi.Entities.Category;
using SP.WebApi.UseCases.Handlers.Category.Dto;

namespace SP.WebApi.UseCases.Handlers.Category.Mappings;

internal static class CategoryMappings
{
    internal static CategoryDto ToDto(CategoryEntity category) =>
        new(category.Id, category.Code, category.Name);
}
