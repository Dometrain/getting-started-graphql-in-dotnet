using eShop.Catalog.Data.EntityConfigurations;

namespace eShop.Catalog.Data;

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    public DbSet<Brand> Brands => Set<Brand>();
    
    public DbSet<BrandSubscription> BrandSubscriptions => Set<BrandSubscription>();
    
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new BrandSubscriptionEntityTypeConfiguration());
        builder.ApplyConfiguration(new ProductTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}

