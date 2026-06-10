using SP.WebApi.Entities;

namespace SP.WebApi.Tests.Entities;

public sealed class ExampleItemTests
{
    [Fact]
    public void Create_WithValidName_ReturnsEntity()
    {
        var item = ExampleItem.Create("test");

        Assert.NotEqual(Guid.Empty, item.Id);
        Assert.Equal("test", item.Name);
    }

    [Fact]
    public void Create_WithEmptyName_Throws()
    {
        Assert.Throws<ArgumentException>(() => ExampleItem.Create("  "));
    }
}
