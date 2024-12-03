using HotChocolate.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddServiceDefaults()
    .AddApplicationServices();

builder
    .AddGraphQL()
    .AddCatalogTypes()
    .AddGraphQLConventions()
    .AddBananaCakePopServices(c =>
    {
        c.ApiId = "QXBpCmcyMmU0ZjNkOGVmMzc0N2E5YWI2ZDMwZmIzMmIzMzViOQ==";
        c.ApiKey = "KHE1EOf1rZVfnKxn471WCJRriEQ7RZAYPb7CYogLmUtSMfWqW3M8DGRnAuaIZY4N";
        c.Stage = "dev";
    })
    .AddInstrumentation(options =>
    {
        options.Scopes = ActivityScopes.All;

    })
    .InitializeOnStartup();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseWebSockets();

app.MapDefaultEndpoints();

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);
