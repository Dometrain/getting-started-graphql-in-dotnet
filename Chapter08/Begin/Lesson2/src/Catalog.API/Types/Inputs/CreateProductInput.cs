namespace eShop.Catalog.Types.Inputs;

public record CreateProductInput(
    string Name,
    string? Description,
    decimal Price,
    [property: ID<ProductType>] int TypeId,
    [property: ID<Brand>] int BrandId,
    int RestockThreshold,
    int MaxStockThreshold);