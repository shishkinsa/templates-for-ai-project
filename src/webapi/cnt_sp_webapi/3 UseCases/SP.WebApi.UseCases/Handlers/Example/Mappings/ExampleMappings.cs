using SP.WebApi.Entities;
using SP.WebApi.UseCases.Handlers.Example.Dto;

namespace SP.WebApi.UseCases.Handlers.Example.Mappings;

internal static class ExampleMappings
{
    internal static ExampleItemDto ToDto(ExampleItem item) =>
        new(item.Id, item.Name, item.CreatedAt);
}
