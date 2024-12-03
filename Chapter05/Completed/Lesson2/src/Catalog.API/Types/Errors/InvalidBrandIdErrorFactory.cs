namespace eShop.Catalog.Types.Errors;

public class InvalidBrandIdErrorFactory : IPayloadErrorFactory<EntityNotFoundException, InvalidBrandIdError?>
{
    public InvalidBrandIdError? CreateErrorFrom(EntityNotFoundException exception)
    {
        if (exception.EntityName == nameof(Brand))
        {
            return new InvalidBrandIdError(exception.EntityId);
        }

        return null;
    }
}