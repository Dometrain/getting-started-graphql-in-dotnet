using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Data.EntityConfigurations;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products")
            .HasKey(ci => ci.Id);
        
        builder
            .Property(ci => ci.Id)
            .ValueGeneratedOnAdd();
        
        builder
            .Property(ci => ci.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder
            .Property(ci => ci.Description)
            .HasMaxLength(2048);
        
        builder
            .Property(ci => ci.ImageFileName)
            .HasMaxLength(256);

        builder
            .HasOne(ci => ci.Brand)
            .WithMany(ci => ci.Products)
            .HasForeignKey(ci => ci.BrandId);

        builder
            .HasOne(ci => ci.Type)
            .WithMany(ci => ci.Products)
            .HasForeignKey(ci => ci.TypeId);

        builder
            .HasIndex(ci => ci.Name);
    }
}
