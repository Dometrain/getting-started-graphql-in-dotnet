var builder = WebApplication.CreateBuilder(args);

builder
    .AddServiceDefaults()
    .AddApplicationServices();

builder
    .AddGraphQL()
    .AddCatalogTypes()
    .AddGraphQLConventions()
    .AddInstrumentation()
    .InitializeOnStartup();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseWebSockets();

app.MapDefaultEndpoints();

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);
