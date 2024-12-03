using eShop.Catalog.Sessions;
using HotChocolate.Execution.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddGraphQLConventions(
        this IRequestExecutorBuilder builder)
    {
        builder.AddPagingArguments();
        builder.AddGlobalObjectIdentification();
        builder.AddMutationConventions();
        builder.AddUploadType();
        builder.AddHttpRequestInterceptor<SessionHttpRequestInterceptor>();
        builder.AddInMemorySubscriptions();
        return builder;
    } 
}