namespace eShop.Catalog.Data;

public sealed class CatalogContextOwner(IAsyncDisposable scope, CatalogContext context) : IAsyncDisposable
{
    private bool _disposed;
    
    public CatalogContext Context => context;
    
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }
        
        await context.DisposeAsync();
        await scope.DisposeAsync();
        _disposed = true;
    }
}