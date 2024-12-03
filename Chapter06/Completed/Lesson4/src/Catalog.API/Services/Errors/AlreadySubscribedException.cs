namespace eShop.Catalog.Services.Errors;

public sealed class AlreadySubscribedException : Exception
{
    public AlreadySubscribedException(int userId, int brandId)
        : base($"The user `{userId}` is already subscribed to `{brandId}`.")
    {
        BrandId = brandId;
        UserId = userId;
    }
    
    public AlreadySubscribedException(int userId, int brandId, Exception innerException)
        : base($"The user `{userId}` is already subscribed to `{brandId}`.", innerException)
    {
        BrandId = brandId;
        UserId = userId;
    }

    public int UserId { get; }
    
    public int BrandId { get; }
}