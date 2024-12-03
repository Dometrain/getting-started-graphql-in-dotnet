using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[QueryType]
public static class BrandQueries
{
    [UsePaging]
    public static async Task<Connection<Brand>> GetBrandsAsync(
        PagingArguments pagingArgs,
        BrandService brandService,
        CancellationToken cancellationToken)
        => await brandService.GetBrandsAsync(pagingArgs, cancellationToken).ToConnectionAsync();

    [NodeResolver]
    public static async Task<Brand?> GetBrandByIdAsync(
        int id,
        BrandService brandService,
        CancellationToken cancellationToken)
        => await brandService.GetBrandByIdAsync(id, cancellationToken);
    
    public static async Task<Brand?> GetBrandByNameAsync(
        string name,
        BrandService brandService,
        CancellationToken cancellationToken)
        => await brandService.GetBrandByNameAsync(name, cancellationToken);
}