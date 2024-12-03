using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[ObjectType<Brand>]
public static partial class BrandNode
{
    [UsePaging]
    public static async Task<Connection<Product>> GetProductsAsync(
        [Parent] Brand brand,
        PagingArguments pagingArgs,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductsByBrandAsync(brand.Id, pagingArgs, cancellationToken).ToConnectionAsync();
}