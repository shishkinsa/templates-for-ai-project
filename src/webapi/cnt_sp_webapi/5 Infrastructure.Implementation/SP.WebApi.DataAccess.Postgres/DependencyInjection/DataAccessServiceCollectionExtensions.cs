using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SP.WebApi.DataAccess.Postgres.Data;
using SP.WebApi.DataAccess.Postgres.Repositories;
using SP.WebApi.Infrastructure.Interfaces.DataAccess;
using SP.WebApi.Infrastructure.Interfaces.Repositories;

namespace SP.WebApi.DataAccess.Postgres.DependencyInjection;

/// <summary>
/// Регистрация инфраструктуры доступа к данным PostgreSQL.
/// </summary>
public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDataAccess(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IExampleItemRepository, ExampleItemRepository>();

        return services;
    }
}
