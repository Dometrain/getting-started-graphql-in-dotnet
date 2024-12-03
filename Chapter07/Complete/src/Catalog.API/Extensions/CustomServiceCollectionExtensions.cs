using eShop.Catalog.Sessions;
using ISession = eShop.Catalog.Sessions.ISession;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(
        this IHostApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices(
            builder.Configuration.GetConnectionString("CatalogDB"));
        return builder;
    }
    
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        string? connectionString = null)
    {
        services
            .AddDbContextPool<CatalogContext>(
                o => o.UseNpgsql(connectionString));

        services
            .AddMigration<CatalogContext, CatalogContextSeed>();

        services
            .AddScoped<BrandService>()
            .AddScoped<ProductService>()
            .AddScoped<ProductTypeService>()
            .AddSingleton<ImageStorage>();

        services
            .AddScoped<DefaultSession>()
            .AddScoped<ISession>(sp => sp.GetRequiredService<DefaultSession>());

        return services;
    }
}