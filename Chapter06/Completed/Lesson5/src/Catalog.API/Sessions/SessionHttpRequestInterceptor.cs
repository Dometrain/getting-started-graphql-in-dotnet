using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace eShop.Catalog.Sessions;

public sealed class SessionHttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override async ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        await base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);

        if (context.Request.Headers.TryGetValue("userId", out var value) && 
            int.TryParse(value, out var userId))
        {
            var session = context.RequestServices.GetRequiredService<DefaultSession>();
            await using var catalogContext = context.RequestServices.GetRequiredService<CatalogContext>();
            var user = await catalogContext.Users.FindAsync([userId], cancellationToken: cancellationToken);
            session.CurrentUser = user;
        } 
    }
}