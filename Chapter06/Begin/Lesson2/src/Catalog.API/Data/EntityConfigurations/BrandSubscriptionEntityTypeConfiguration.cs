using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Data.EntityConfigurations;

internal sealed class BrandSubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<BrandSubscription>
{
    public void Configure(EntityTypeBuilder<BrandSubscription> builder)
    {
        builder
            .ToTable("BrandSubscriptions")
            .HasKey(s => new { s.BrandId, s.UserId });
        
        builder
            .HasOne(u => u.User);
        
        builder
            .HasOne(u => u.Brand);
    }
}