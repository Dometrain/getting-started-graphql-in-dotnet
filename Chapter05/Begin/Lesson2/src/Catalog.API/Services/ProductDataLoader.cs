using HotChocolate.Pagination;

namespace eShop.Catalog.Services;

[DataLoaderGroup("ProductBatchingContext")]
internal static class ProductDataLoader
{
    [DataLoader]
    public static async Task<Dictionary<int, Product>> GetProductByIdAsync(
        IReadOnlyList<int> ids,
        CatalogContext context,
        CancellationToken ct)
        => await context.Products
            .AsNoTracking()
            .Where(t => ids.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, ct);

    [DataLoader]
    public static async Task<Dictionary<int, Page<Product>>> GetProductsByBrandIdAsync(
        IReadOnlyList<int> brandIds,
        PagingArguments pagingArgs,
        CatalogContext context,
        CancellationToken ct)
        => await context.Products
            .AsNoTracking()
            .Where(t => brandIds.Contains(t.BrandId))
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .ToBatchPageAsync(t => t.BrandId, pagingArgs, ct);
    
    [DataLoader]
    public static async Task<Dictionary<int, Page<Product>>> GetProductsByTypeIdAsync(
        IReadOnlyList<int> typeIds,
        PagingArguments pagingArgs,
        CatalogContext context,
        CancellationToken ct)
        => await context.Products
            .AsNoTracking()
            .Where(t => typeIds.Contains(t.TypeId))
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .ToBatchPageAsync(t => t.TypeId, pagingArgs, ct);
}
