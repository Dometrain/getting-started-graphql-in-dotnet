using eShop.Catalog.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Design;

namespace eShop.Catalog.Data;

public class CatalogContext(DbContextOptions<CatalogContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    public DbSet<Brand> Brands => Set<Brand>();
    
    public DbSet<BrandSubscription> BrandSubscriptions => Set<BrandSubscription>();
    
    public DbSet<User> Users => Set<User>();
    
    public DbSet<CustomerChat> Chats => Set<CustomerChat>();
    
    public DbSet<CustomerChatMessage> ChatMessages => Set<CustomerChatMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new BrandSubscriptionEntityTypeConfiguration());
        builder.ApplyConfiguration(new ProductTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        builder.ApplyConfiguration(new CustomerChatEntityTypeConfiguration());
        builder.ApplyConfiguration(new CustomerChatMessageEntityTypeConfiguration());
    }
}
