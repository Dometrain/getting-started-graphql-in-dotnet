using HotChocolate.Pagination;

namespace eShop.Catalog.Services;

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
    public static async Task<Dictionary<PageKey<int>, Page<Product>>> GetProductsByBrandIdAsync(
        IReadOnlyList<PageKey<int>> keys,
        CatalogContext context,
        CancellationToken ct)
    {
        Dictionary<PageKey<int>, Page<Product>>? result = null;
        
        foreach (var group in keys.GroupBy(t => t.PagingArgs))
        {
            var brandIds = group.Select(t => t.Key).ToArray();
            
            var groupResult = await context.Brands
                .AsNoTracking()
                .Where(t => brandIds.Contains(t.Id))
                .Include(t => t.Products.OrderBy(p => p.Name).ThenBy(p => p.Id))
                .ToBatchPageAsync(t => t.Id, group.Key, ct);
            
            if(result is null)
            {
                result = groupResult;
            }
            else
            {
                foreach (var item in groupResult)
                {
                    result.Add(item.Key, item.Value);
                }
            }
        }

        result ??= new Dictionary<PageKey<int>, Page<Product>>();
        
        foreach (var key in keys)
        {
            result.TryAdd(key, Page<Product>.Empty);
        }

        return result;
    }
    
    [DataLoader]
    public static async Task<Dictionary<PageKey<int>, Page<Product>>> GetProductsByTypeIdAsync(
        IReadOnlyList<PageKey<int>> keys,
        CatalogContext context,
        CancellationToken ct)
    {
        Dictionary<PageKey<int>, Page<Product>>? result = null;
        
        foreach (var group in keys.GroupBy(t => t.PagingArgs))
        {
            var typeIds = group.Select(t => t.Key).ToArray();
            
            var groupResult = await context.ProductTypes
                .AsNoTracking()
                .Where(t => typeIds.Contains(t.Id))
                .Include(t => t.Products.OrderBy(p => p.Name).ThenBy(p => p.Id))
                .ToBatchPageAsync(t => t.Id, group.Key, ct);
            
            if(result is null)
            {
                result = groupResult;
            }
            else
            {
                foreach (var item in groupResult)
                {
                    result.Add(item.Key, item.Value);
                }
            }
        }

        result ??= new Dictionary<PageKey<int>, Page<Product>>();
        
        foreach (var key in keys)
        {
            result.TryAdd(key, Page<Product>.Empty);
        }

        return result;
    }
}
