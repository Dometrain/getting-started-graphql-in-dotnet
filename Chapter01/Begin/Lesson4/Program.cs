var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<PetRepository>();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddObjectType<Dog>()
    .AddObjectType<Cat>()
    .AddObjectType<Parrot>()
    .ModifyOptions(options => options.EnableOneOf = true)
    .ModifyPagingOptions(o => o.RequirePagingBoundaries = false);

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
