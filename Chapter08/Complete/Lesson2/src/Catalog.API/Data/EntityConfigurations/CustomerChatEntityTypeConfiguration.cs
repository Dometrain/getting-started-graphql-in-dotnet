using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Catalog.Data.EntityConfigurations;

internal sealed class CustomerChatEntityTypeConfiguration : IEntityTypeConfiguration<CustomerChat>
{
    public void Configure(EntityTypeBuilder<CustomerChat> builder)
    {
        builder
            .ToTable("CustomerChats");

        builder
            .Property(t => t.CustomerName)
            .HasMaxLength(100);

        builder
            .HasIndex(t => t.SessionId)
            .IsUnique();

        builder
            .HasMany(t => t.Messages)
            .WithOne(t => t.Chat)
            .HasForeignKey(t => t.ChatId);
    }
}