var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<CatalogContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

builder.Services
    .AddMigration<CatalogContext, CatalogContextSeed>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<ProductFilterInputType>()
    .AddType<ProductSortInputType>()
    .AddGraphQLConventions();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);