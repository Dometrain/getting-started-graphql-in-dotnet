namespace eShop.Catalog.Types.Filtering;

public readonly record struct ProductsFilterInputType(
    ProductsBrandIdFilterInputType? BrandId,
    ProductsTypeIdFilterInputType? TypeId)
{
    public ProductFilter ToFilter() => new(BrandId?.In, TypeId?.In);
}

public readonly record struct ProductsBrandIdFilterInputType(int[]? In);

public readonly record struct ProductsTypeIdFilterInputType(int[]? In);