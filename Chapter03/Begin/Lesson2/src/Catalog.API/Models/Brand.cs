// ReSharper disable CollectionNeverUpdated.Global
using System.ComponentModel.DataAnnotations;
using eShop.Catalog.Types.Configuration;

namespace eShop.Catalog.Models;

public sealed class Brand
{
    public int Id { get; set; }

    [Required]
    [UseToUpper]
    public string Name { get; set; } = default!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
