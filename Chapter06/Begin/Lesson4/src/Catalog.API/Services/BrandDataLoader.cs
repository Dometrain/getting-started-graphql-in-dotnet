namespace eShop.Catalog.Services;

internal static class BrandDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Brand>> GetBrandByIdAsync(
        IReadOnlyList<int> ids,
        CatalogContext context,
        CancellationToken ct)
        => await context.Brands
            .AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, ct);
    
    [DataLoader]
    public static async Task<Dictionary<string, Brand>> GetBrandByNameAsync(
        IReadOnlyList<string> names,
        CatalogContext context,
        CancellationToken ct)
        => await context.Brands
            .AsNoTracking()
            .Where(t => names.Contains(t.Name))
            .ToDictionaryAsync(t => t.Name, ct);
}