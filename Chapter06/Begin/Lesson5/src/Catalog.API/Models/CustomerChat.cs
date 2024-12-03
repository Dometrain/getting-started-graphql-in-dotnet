using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.Models;

public sealed class CustomerChat
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    public Guid SessionId { get; set; }

    [Required] 
    public required string CustomerName { get; set; }

    public DateTime Time { get; set; }
    
    public int AssignedTo { get; set; }

    public ICollection<CustomerChatMessage> Messages { get; set; } = [];
}