namespace eShop.Catalog.Services;

public readonly record struct ProductFilter(
    IReadOnlyList<int>? BrandIds,
    IReadOnlyList<int>? TypeIds);