using eShop.Catalog.Services.Errors;

namespace eShop.Catalog.Types.Errors;

public record InvalidBrandIdError([property: ID<Brand>] int Id)
{
    public string Message => "The provided brand id is invalid.";
    
    public static InvalidBrandIdError? CreateErrorFrom(EntityNotFoundException exception)
    {
        if (exception.EntityName == nameof(Brand))
        {
            return new InvalidBrandIdError(exception.EntityId);
        }

        return null;
    }
}