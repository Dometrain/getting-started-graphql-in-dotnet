namespace eShop.Catalog.Types.Errors;

public record InvalidBrandIdError([property: ID<Brand>] int Id)
{
    public string Message => "The provided brand id is invalid.";
}