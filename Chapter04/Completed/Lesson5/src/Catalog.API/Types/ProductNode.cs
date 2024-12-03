using HotChocolate.Resolvers;

namespace eShop.Catalog.Types;

[ObjectType<Product>]
public static partial class ProductNode
{
    static partial void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Ignore(t => t.BrandId)
            .Ignore(t => t.TypeId)
            .Ignore(t => t.AddStock(default))
            .Ignore(t => t.RemoveStock(default));
    }

    public static int InternalId([Parent] Product product) => product.Id;

    public static string Errors(IResolverContext context)
    {
        throw new GraphQLException(
            ErrorBuilder.New()
                .SetMessage("Something is wrong 1!")
                .SetPath(context.Path)
                .AddLocation(
                    context.Selection.SyntaxNode.Location!.Line,
                    context.Selection.SyntaxNode.Location!.Column)
                .Build(),
            ErrorBuilder.New()
                .SetMessage("Something is wrong 2!")
                .SetPath(context.Path)
                .AddLocation(
                    context.Selection.SyntaxNode.Location!.Line,
                    context.Selection.SyntaxNode.Location!.Column)
                .Build());
    }
    
    public static string? NullableErrors() => throw new ThisIsNiceException();

    [BindMember(nameof(Product.Brand))]
    public static async Task<Brand?> GetBrandAsync(
        [Parent] Product product, 
        BrandService brandService, 
        CancellationToken cancellationToken)
        => await brandService.GetBrandByIdAsync(product.BrandId, cancellationToken);
    
    [BindMember(nameof(Product.Type))]
    public static async Task<ProductType?> GetProductTypeAsync(
        [Parent] Product product, 
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypeByIdAsync(product.BrandId, cancellationToken);
}

public record NotFound(string Message, [ID<Product>] int Id);

public class ThisIsNiceException : Exception
{
    public string SomethingUseful => "Something Broke";
}