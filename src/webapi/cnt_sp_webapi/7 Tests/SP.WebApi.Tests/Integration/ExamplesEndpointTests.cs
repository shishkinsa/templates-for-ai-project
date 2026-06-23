using System.Net;
using System.Net.Http.Json;

namespace SP.WebApi.Tests.Integration;

public sealed class ExamplesEndpointTests(WebApiFactory factory) : IClassFixture<WebApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetHealth_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/v1/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateAndListExample_Works()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/api/v1/examples",
            new { name = "Integration test" });

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var listResponse = await _client.GetAsync("/api/v1/examples");
        listResponse.EnsureSuccessStatusCode();

        var list = await listResponse.Content.ReadFromJsonAsync<ListExamplesPayload>();
        Assert.NotNull(list);
        Assert.Contains(list!.Items, x => x.Name == "Integration test");
    }

    [Fact]
    public async Task UpdateAndDeleteExample_Works()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/api/v1/examples",
            new { name = "Before update" });
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<CreateExamplePayload>();
        Assert.NotNull(created);

        var updateResponse = await _client.PutAsJsonAsync(
            $"/api/v1/examples/{created!.Item.Id}",
            new { name = "After update" });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/api/v1/examples/{created.Item.Id}");
        getResponse.EnsureSuccessStatusCode();
        var getPayload = await getResponse.Content.ReadFromJsonAsync<GetExamplePayload>();
        Assert.Equal("After update", getPayload!.Item.Name);

        var deleteResponse = await _client.DeleteAsync($"/api/v1/examples/{created.Item.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var notFoundResponse = await _client.GetAsync($"/api/v1/examples/{created.Item.Id}");
        Assert.Equal(HttpStatusCode.NotFound, notFoundResponse.StatusCode);
    }

    private sealed record ListExamplesPayload(IReadOnlyList<ExampleItemPayload> Items);

    private sealed record CreateExamplePayload(ExampleItemPayload Item);

    private sealed record GetExamplePayload(ExampleItemPayload Item);

    private sealed record ExampleItemPayload(Guid Id, string Name, DateTimeOffset CreatedAt);
}
