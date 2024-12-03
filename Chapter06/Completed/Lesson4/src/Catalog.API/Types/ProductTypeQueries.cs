using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[QueryType]
public static class ProductTypeQueries
{
    [UsePaging]
    public static async Task<Connection<ProductType>> GetProductTypesAsync(
        PagingArguments pagingArguments,
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypesAsync(pagingArguments, cancellationToken).ToConnectionAsync();

    [NodeResolver]
    public static async Task<ProductType?> GetProductTypeByIdAsync(
        int id,
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypeByIdAsync(id, cancellationToken);
    
    public static async Task<ProductType?> GetProductTypeByNameAsync(
        string name,
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypeByNameAsync(name, cancellationToken);
}