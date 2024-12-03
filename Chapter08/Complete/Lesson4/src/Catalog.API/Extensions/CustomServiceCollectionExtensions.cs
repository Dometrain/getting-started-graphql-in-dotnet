using eShop.Catalog.Sessions;
using ISession = eShop.Catalog.Sessions.ISession;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(
        this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>("catalog-db");
        
        builder.Services
            .AddMigration<CatalogContext, CatalogContextSeed>();
        
        builder.Services
            .AddHostedService<ImageProcessingService>();

        builder.Services
            .AddScoped<BrandService>()
            .AddScoped<ProductService>()
            .AddScoped<ProductTypeService>()
            .AddScoped<ChatService>()
            .AddSingleton<ImageStorage>();

        builder.Services
            .AddScoped<DefaultSession>()
            .AddScoped<ISession>(sp => sp.GetRequiredService<DefaultSession>());

        return builder;
    }
}