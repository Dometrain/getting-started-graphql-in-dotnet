namespace eShop.Catalog.Types.Inputs;

public record UpdateProductInput(
    [property: ID<Product>] int Id,
    [property: DefaultValue("")] Optional<string> Name,
    Optional<string?> Description,
    [property: DefaultValue(0.0)] Optional<decimal> Price,
    [property: DefaultValue("UHJvZHVjdFR5cGUKaTQ=")] [property: ID<ProductType>] Optional<int> TypeId,
    [property: DefaultValue("QnJhbmQKaTk=")] [property: ID<Brand>] Optional<int> BrandId,
    [property: DefaultValue(0)] Optional<int> RestockThreshold,
    [property: DefaultValue(0)] Optional<int> MaxStockThreshold);