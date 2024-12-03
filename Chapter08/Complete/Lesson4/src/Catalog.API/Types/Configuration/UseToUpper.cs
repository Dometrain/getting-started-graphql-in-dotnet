using HotChocolate.Types.Descriptors.Definitions;

namespace eShop.Catalog.Types.Configuration;

public static class UseToUpperObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseToUpper(this IObjectFieldDescriptor descriptor)
    {
        var definition = descriptor.Extend().Definition;
        
        definition.FormatterDefinitions.Add(
            new ResultFormatterDefinition(
                (_, result) =>
                {
                    if (result is string s)
                    {
                        result = s.ToUpperInvariant();
                    }
                    
                    return result;
                },
                isRepeatable: false,
                key: nameof(UseToUpper)));

        return descriptor;
    }
}