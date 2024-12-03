namespace eShop.Catalog.Services;

public sealed class ProductTypeService(
    CatalogContext context,
    IProductTypeByIdDataLoader productTypeById,
    IProductTypeByNameDataLoader productTypeByName)
{
    public async Task<ProductType?> GetProductTypeByIdAsync(
        int id, 
        CancellationToken cancellationToken = default)
        => await productTypeById.LoadAsync(id, cancellationToken);
    
    public async Task<ProductType?> GetProductTypeByNameAsync(
        string name, 
        CancellationToken cancellationToken = default)
        => await productTypeByName.LoadAsync(name, cancellationToken);
    
    public async Task<Page<ProductType>> GetProductTypesAsync(
        PagingArguments pagingArguments,
        CancellationToken cancellationToken = default) 
        => await context.ProductTypes
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ThenBy(t => t.Id)
            .ToPageAsync(pagingArguments, cancellationToken);
    
    public async Task CreateProductTypeAsync(ProductType type, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(type.Name))
        {
            ArgumentException.ThrowIfNullOrEmpty(type.Name);
        }
        
        context.ProductTypes.Add(type);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateProductTypeAsync(ProductType type, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(type.Name))
        {
            ArgumentException.ThrowIfNullOrEmpty(type.Name);
        }

        context.ProductTypes.Entry(type).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> DeleteProductTypeAsync(int id, CancellationToken cancellationToken)
    {
        var affectedRows = await context.ProductTypes.Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken);
        return affectedRows > 0;
    }
}