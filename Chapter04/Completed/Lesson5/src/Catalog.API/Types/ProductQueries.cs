using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;

namespace eShop.Catalog.Types;

[QueryType]
public static class ProductQueries
{
    [UsePaging]
    public static async Task<Connection<Product>> GetProductsAsync(
        ProductsFilterInputType? where,
        PagingArguments pagingArgs,
        ProductService productService, 
        CancellationToken cancellationToken)
        => await productService.GetProductsAsync(where?.ToFilter(), pagingArgs, cancellationToken).ToConnectionAsync();

    [Error<CustomError>]
    [NodeResolver]
    public static async Task<Product?> GetProductByIdAsync(
        int id,
        ProductService productService,
        CancellationToken cancellationToken)
        => await productService.GetProductByIdAsync(id, cancellationToken);
}

public class CustomError : IMyErrorInterface
{
    private readonly CustomException _exception;

    public CustomError(CustomException exception)
    {
        _exception = exception;
    }

    public string Message => "This is a safe message";

    public int Id => _exception.Id;
}

public record IdNotValid(int Id)
{
    public string Message => "Invalid Id Structure";
}
