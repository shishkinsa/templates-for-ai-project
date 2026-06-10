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

    private sealed record ListExamplesPayload(IReadOnlyList<ExampleItemPayload> Items);

    private sealed record ExampleItemPayload(Guid Id, string Name, DateTimeOffset CreatedAt);
}
