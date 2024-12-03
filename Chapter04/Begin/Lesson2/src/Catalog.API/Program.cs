var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContextPool<CatalogContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

builder.Services
    .AddMigration<CatalogContext, CatalogContextSeed>();

builder.Services
    .AddScoped<BrandService>()
    .AddScoped<ProductService>()
    .AddScoped<ProductTypeService>()
    .AddTransient<CatalogContextOwner>(sp =>
    {
        var scope = sp.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
        return new CatalogContextOwner(scope, context);
    });

builder.Services
    .AddGraphQLServer()
    .AddCatalogTypes()
    .AddGraphQLConventions();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);