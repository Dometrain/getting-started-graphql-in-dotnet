var builder = WebApplication.CreateBuilder(args);

builder
    .AddApplicationServices();

builder
    .AddGraphQL()
    .AddCatalogTypes()
    .AddGraphQLConventions()
    .InitializeOnStartup();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);
