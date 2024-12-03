using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace eShop.Catalog.Sessions;

public class SessionHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override async ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        await base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        
        var session = context.RequestServices.GetRequiredService<DefaultSession>();
        await using var catalogContext = context.RequestServices.GetRequiredService<CatalogContext>();
        var user = await catalogContext.Users.FindAsync([1], cancellationToken: cancellationToken);
        session.CurrentUser = user;
    }
}