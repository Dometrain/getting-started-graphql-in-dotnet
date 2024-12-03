using eShop.Catalog.Services.Errors;

namespace eShop.Catalog.Types.Errors;

public record InvalidProductTypeIdError([property: ID<Brand>] int Id)
{
    public string Message => "The provided product type id is invalid.";
    
    public static InvalidProductTypeIdError? CreateErrorFrom(EntityNotFoundException exception)
    {
        if (exception.EntityName == nameof(ProductType))
        {
            return new InvalidProductTypeIdError(exception.EntityId);
        }

        return null;
    }
}