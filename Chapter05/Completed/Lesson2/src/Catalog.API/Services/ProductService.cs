using GreenDonut.Selectors;
using HotChocolate.Pagination;

namespace eShop.Catalog.Services;

public sealed class ProductService(
    CatalogContext context, 
    IProductBatchingContext batching)
{
    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        => await batching.ProductById.LoadAsync(id, cancellationToken);
    
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

    public async Task CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(product.Name);
        
        if (product.RestockThreshold >= product.MaxStockThreshold)
        {
            throw new MaxStockThresholdToSmallException(product.RestockThreshold, product.MaxStockThreshold);
        }
        
        if (!await context.Brands.AnyAsync(t => t.Id == product.BrandId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Brand), product.BrandId);
        }
        
        if (!await context.ProductTypes.AnyAsync(t => t.Id == product.TypeId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(ProductType), product.TypeId);
        }
        
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        if (product.Id < 1)
        {
            throw new InvalidOperationException("Invalid product id.");
        }
        
        ArgumentException.ThrowIfNullOrEmpty(product.Name);
        
        if (product.RestockThreshold >= product.MaxStockThreshold)
        {
            throw new MaxStockThresholdToSmallException(product.RestockThreshold, product.MaxStockThreshold);
        }
        
        if (!await context.Brands.AnyAsync(t => t.Id == product.BrandId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(Brand), product.BrandId);
        }
        
        if (!await context.ProductTypes.AnyAsync(t => t.Id == product.TypeId, cancellationToken))
        {
            throw new EntityNotFoundException(nameof(ProductType), product.TypeId);
        }

        context.Products.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }
}
