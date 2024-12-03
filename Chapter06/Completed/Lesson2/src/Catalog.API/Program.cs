var builder = WebApplication.CreateBuilder(args);

builder
    .AddApplicationServices();

builder
    .AddGraphQL()
    .AddCatalogTypes()
    .AddGraphQLConventions()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);
