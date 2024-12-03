using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace eShop.Catalog.Types;

public class Query
{
    [UsePaging(DefaultPageSize = 1, MaxPageSize = 10)]
    [UseProjection]
    public IQueryable<Brand> GetBrands(CatalogContext context)
        => context.Brands;
    
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Brand> GetBrandById(int id, CatalogContext context)
        => context.Brands.Where(t => t.Id == id);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts(CatalogContext context, IFilterContext filterContext, ISortingContext sortingContext)
    {
        filterContext.Handled(false);
        sortingContext.Handled(false);

        IQueryable<Product> query = context.Products;

        if (!filterContext.IsDefined)
        {
            query = query.Where(t => t.BrandId == 1);
        }

        if (!sortingContext.IsDefined)
        {
            query = query.OrderBy(t => t.Brand!.Name).ThenByDescending(t => t.Price);
        }

        return query;
    }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Product> GetProductById(int id, CatalogContext context)
        => context.Products.Where(t => t.Id == id);
    
    [UsePaging]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypes(CatalogContext context)
        => context.ProductTypes;
    
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductType> GetProductTypeById(int id, CatalogContext context)
        => context.ProductTypes.Where(t => t.Id == id);
}