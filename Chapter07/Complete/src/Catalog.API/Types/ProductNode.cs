namespace eShop.Catalog.Types;

[ObjectType<Product>]
public static partial class ProductNode
{
    static partial void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Ignore(t => t.BrandId)
            .Ignore(t => t.TypeId)
            .Ignore(t => t.ImageFileName)
            .Ignore(t => t.AddStock(default))
            .Ignore(t => t.RemoveStock(default));
    }

    public static Uri GetImageUrl([Parent] Product product, HttpContext context) 
        => new($"http://{context.Request.Host}/api/products/{product.Id}/image");

    public static async Task<Brand?> GetBrandAsync(
        [Parent] Product product, 
        BrandService brandService, 
        CancellationToken cancellationToken)
        => await brandService.GetBrandByIdAsync(product.BrandId, cancellationToken);
    
    public static async Task<ProductType?> GetProductTypeAsync(
        [Parent] Product product, 
        ProductTypeService productTypeService,
        CancellationToken cancellationToken)
        => await productTypeService.GetProductTypeByIdAsync(product.BrandId, cancellationToken);
}
