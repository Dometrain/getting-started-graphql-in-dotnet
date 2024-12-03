using HotChocolate.Execution.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class CustomRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddGraphQLConventions(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddProjections()
            .AddFiltering(
                c => c.AddDefaults()
                    .BindRuntimeType<string, DefaultStringOperationFilterInputType>())
            .AddSorting();
        return builder;
    } 
}