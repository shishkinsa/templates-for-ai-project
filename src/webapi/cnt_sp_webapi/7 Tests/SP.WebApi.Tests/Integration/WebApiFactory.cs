using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SP.WebApi.DataAccess.Postgres.Data;
using SP.WebApi.DataAccess.Postgres.Repositories;
using SP.WebApi.Infrastructure.Interfaces.DataAccess;
using SP.WebApi.Infrastructure.Interfaces.Repositories;

namespace SP.WebApi.Tests.Integration;

/// <summary>
/// Фабрика тестового хоста с InMemory БД.
/// </summary>
public sealed class WebApiFactory : WebApplicationFactory<Program>
{
    public WebApiFactory()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", string.Empty);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = string.Empty,
            });
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(AppDbContext));
            services.RemoveAll(typeof(IDbContext));
            services.RemoveAll(typeof(IExampleItemRepository));

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("WebApiTests"));
            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<AppDbContext>());
            services.AddScoped<IExampleItemRepository, ExampleItemRepository>();
        });
    }
}
