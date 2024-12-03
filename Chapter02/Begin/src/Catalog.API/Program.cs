var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<CatalogContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDB")));

builder.Services
    .AddMigration<CatalogContext, CatalogContextSeed>();

builder.Services
    .AddGraphQLServer()
    .ModifyPagingOptions(o => o.RequirePagingBoundaries = false);

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);