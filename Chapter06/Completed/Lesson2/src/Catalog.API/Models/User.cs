using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.Models;

public class User
{
    public int Id { get; set; } 
        
    [Required]
    public string Name { get; set; } = default!;
    
    public int? ReceivedArrivalNotificationId { get; set; }
    
    public ICollection<BrandSubscription> Subscriptions { get; set; } = new List<BrandSubscription>(); 
}