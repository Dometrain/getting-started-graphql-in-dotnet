var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryType>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
