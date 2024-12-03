var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .ModifyPagingOptions(o => o.RequirePagingBoundaries = false);

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
