using HotChocolate.Pagination;

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
}