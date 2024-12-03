using eShop.Catalog.Sessions;
using ISession = eShop.Catalog.Sessions.ISession;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(
        this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddDbContextPool<CatalogContext>(
                o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

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