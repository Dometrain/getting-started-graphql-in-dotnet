var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRepositories();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
