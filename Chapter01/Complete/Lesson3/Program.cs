var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .ModifyPagingOptions(o => o.RequirePagingBoundaries = false);

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class Query
{
    public string SayHello(string name = "World")
        => $"Hello {name}";
}