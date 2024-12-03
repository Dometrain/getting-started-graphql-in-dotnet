using GreenDonut.Selectors;
using HotChocolate.Pagination;

namespace eShop.Catalog.Services;

public sealed class ProductService(
    CatalogContext context, 
    IProductBatchingContext batching)
{
    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new CustomException();

        // return await batching.ProductById.LoadAsync(id, cancellationToken);
    }
    
    public async Task<Page<Product>> GetProductsAsync(
        ProductFilter? productFilter,
        PagingArguments pagingArguments,  
        CancellationToken cancellationToken = default)
    {
        var query = context.Products.AsNoTracking();

        if (productFilter?.BrandIds is { Count: > 0 } brandIds)
        {
            query = query.Where(p => brandIds.Contains(p.BrandId));
        }
        
        if (productFilter?.TypeIds is { Count: > 0 } typeIds)
        {
            query = query.Where(p => typeIds.Contains(p.TypeId));
        }
        
        return await query.OrderBy(t => t.Name).ThenBy(t => t.Id).ToPageAsync(pagingArguments, cancellationToken);
    }

    public async Task<Page<Product>?> GetProductsByBrandAsync(
        int brandId,
        PagingArguments pagingArgs,
        CancellationToken ct = default)
        => await batching.ProductsByBrandId.WithPagingArguments(pagingArgs).LoadAsync(brandId, ct);

    public async Task<Page<Product>?> GetProductsByTypeAsync(
        int typeId,
        PagingArguments pagingArgs,
        CancellationToken ct = default)
        => await batching.ProductsByTypeId.WithPagingArguments(pagingArgs).LoadAsync(typeId, ct);
}

public class CustomException : Exception
{
    public int Id => 1;
}