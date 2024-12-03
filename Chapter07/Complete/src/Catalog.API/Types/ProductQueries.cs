using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[QueryType]
public static class ProductQueries
{
    [UsePaging]
    public static async Task<Connection<Product>> GetProductsAsync(
        ProductsFilterInputType? where,
        PagingArguments pagingArguments,
        ProductService productService, 
        CancellationToken cancellationToken)
        => await productService.GetProductsAsync(where?.ToFilter(), pagingArguments, cancellationToken).ToConnectionAsync();

    [NodeResolver]
    public static async Task<Product?> GetProductByIdAsync(
        int id,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductByIdAsync(id, cancellationToken);
}
