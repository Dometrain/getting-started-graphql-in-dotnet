var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddProject<Projects.eShop_Catalog_API>("catalog-api");

builder.Build().Run();