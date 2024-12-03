using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Data.EntityConfigurations;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");
        
        builder
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(u => u.Name)
            .HasMaxLength(100);

        builder
            .HasMany(u => u.Subscriptions);
    }
}