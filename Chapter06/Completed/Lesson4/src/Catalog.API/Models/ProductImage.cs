namespace eShop.Catalog.Models;

public sealed record ProductImage(string Name, Func<Stream> OpenStream);