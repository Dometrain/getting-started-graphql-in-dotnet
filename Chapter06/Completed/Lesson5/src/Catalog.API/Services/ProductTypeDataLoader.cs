namespace eShop.Catalog.Services;

internal static class ProductTypeDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, ProductType>> GetProductTypeByIdAsync(
        IReadOnlyList<int> ids,
        CatalogContext context,
        CancellationToken ct)
        => await context.ProductTypes
            .AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, ct);
    
    [DataLoader]
    public static async Task<Dictionary<string, ProductType>> GetProductTypeByNameAsync(
        IReadOnlyList<string> names,
        CatalogContext context,
        CancellationToken ct)
        => await context.ProductTypes
            .AsNoTracking()
            .Where(t => names.Contains(t.Name))
            .ToDictionaryAsync(t => t.Name, ct);
}