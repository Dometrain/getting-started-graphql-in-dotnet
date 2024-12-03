var builder = WebApplication.CreateBuilder(args);

builder
    .AddApplicationServices();

builder
    .AddGraphQL()
    .AddCatalogTypes()
    .AddGraphQLConventions()
    .InitializeOnStartup();

builder.Services.AddHostedService<ImageProcessingService>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseWebSockets();

app.MapGraphQL();

app.MapImageRoute();

app.RunWithGraphQLCommands(args);
