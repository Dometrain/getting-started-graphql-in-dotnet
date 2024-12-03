namespace eShop.Catalog.Types;

[MutationType]
public static class BrandMutations
{
    public static async Task<Brand> CreateBrandAsync(
        string name, 
        BrandService brandService,
        CancellationToken cancellationToken)
    {
        var brand = new Brand { Name = name };
        await brandService.CreateBrandAsync(brand, cancellationToken);
        return brand;
    }
}
