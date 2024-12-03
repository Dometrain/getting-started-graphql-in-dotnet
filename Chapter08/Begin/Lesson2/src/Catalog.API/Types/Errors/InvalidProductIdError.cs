namespace eShop.Catalog.Types.Errors;

public record InvalidProductIdError([property: ID<Product>] int Id)
{
    public string Message => "The provided product id is invalid.";
}