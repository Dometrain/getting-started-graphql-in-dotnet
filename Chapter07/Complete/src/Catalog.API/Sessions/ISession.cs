namespace eShop.Catalog.Sessions;

public interface ISession
{
    public User? CurrentUser { get; }
}