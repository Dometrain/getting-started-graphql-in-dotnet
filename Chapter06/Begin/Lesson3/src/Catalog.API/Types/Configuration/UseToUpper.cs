namespace eShop.Catalog.Types.Configuration;

public static class UseToUpperObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseToUpper(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async context =>
        {
            await next(context);

            if (context.Result is string s)
            {
                context.Result = s.ToUpperInvariant();
            }
        });
    }
}