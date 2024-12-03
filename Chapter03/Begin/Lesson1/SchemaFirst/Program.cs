var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddDocumentFromFile("./Schema.graphql")
    .BindRuntimeType<Query>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
