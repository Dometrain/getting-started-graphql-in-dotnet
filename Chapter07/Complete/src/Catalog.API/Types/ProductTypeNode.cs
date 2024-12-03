using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[ObjectType<ProductType>]
public static partial class ProductTypeNode
{
    [UsePaging(ConnectionName = "ProductTypeProducts")]
    public static Task<Connection<Product>> GetProductsAsync(
        [Parent] ProductType productType,
        PagingArguments pagingArguments,
        ProductService productService,
        CancellationToken cancellationToken)
        => productService.GetProductsByTypeAsync(productType.Id, pagingArguments, cancellationToken).ToConnectionAsync();
}