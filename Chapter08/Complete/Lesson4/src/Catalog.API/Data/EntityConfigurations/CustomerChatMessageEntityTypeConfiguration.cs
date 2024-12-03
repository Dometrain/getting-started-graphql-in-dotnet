using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Data.EntityConfigurations;

internal sealed class CustomerChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<CustomerChatMessage>
{
    public void Configure(EntityTypeBuilder<CustomerChatMessage> builder)
    {
        builder
            .ToTable("CustomerChatMessages");

        builder
            .Property(t => t.From)
            .HasMaxLength(100);
        
        builder
            .Property(t => t.Text)
            .HasMaxLength(1024);
    }
}