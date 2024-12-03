var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRepositories();

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .InitializeOnStartup()
    .AddNodeIdValueSerializer<BookChapterIdSerializer>();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
