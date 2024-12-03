namespace eShop.Catalog.Models;

public sealed class CustomerChatMessage
{
    public int Id { get; set; }
    
    public required string From { get; set; }
    
    public required string Text { get; set; }
    
    public int ChatId { get; set; }
    
    public CustomerChat? Chat { get; set; }
}