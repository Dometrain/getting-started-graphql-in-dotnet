namespace eShop.Catalog.Sessions;

public sealed class DefaultSession : ISession
{
    public User? CurrentUser { get; set; }
}