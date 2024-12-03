using eShop.Catalog.Services.Errors;
using eShop.Catalog.Types.Errors;
using HotChocolate.Resolvers;

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
    
    public static async Task<FieldResult<Brand, InvalidBrandIdError>> RenameBrandAsync(
        [ID<Brand>] int id,
        string name, 
        BrandService brandService,
        CancellationToken cancellationToken)
    {
        var brand = await brandService.GetBrandByIdAsync(id, cancellationToken);

        if (brand is null)
        {
            return new InvalidBrandIdError(id);
        }

        brand.Name = name;
        await brandService.UpdateBrandAsync(brand, cancellationToken);
        return brand;
    }

    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<FieldResult<bool, InvalidBrandIdError>> DeleteBrandAsync(
        [ID<Brand>] int id,
        BrandService brandService,
        CancellationToken cancellationToken)
    {
        if (await brandService.DeleteBrandAsync(id, cancellationToken))
        {
            return true;
        }

        return new InvalidBrandIdError(id);
    }
    
    [Error<InvalidBrandIdError>]
    [Error<AlreadySubscribedException>]
    [Error<UserSessionRequiredError>]
    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<bool> SubscribeToBrandAsync(
        [ID<Brand>] int id,
        BrandService brandService,
        CancellationToken cancellationToken)
    {
        await brandService.SubscribeToBrandAsync(id, cancellationToken);
        return true;
    }
    
    [Error<InvalidBrandIdError>]
    [Error<UserSessionRequiredError>]
    [UseMutationConvention(PayloadFieldName = "success")]
    public static async Task<bool> UnsubscribeToBrandAsync(
        [ID<Brand>] int id,
        BrandService brandService,
        CancellationToken cancellationToken)
    {
        await brandService.UnsubscribeToBrandAsync(id, cancellationToken);
        return true;
    }
}
