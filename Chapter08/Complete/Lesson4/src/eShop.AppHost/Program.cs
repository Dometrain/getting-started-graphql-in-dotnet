var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin();

var catalogDb = postgres
    .AddDatabase("catalog-db");

builder
    .AddProject<Projects.eShop_Catalog_API>("catalog-api")
    .WithReference(catalogDb);

builder.Build().Run();