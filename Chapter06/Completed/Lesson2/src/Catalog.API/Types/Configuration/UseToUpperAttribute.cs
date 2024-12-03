using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Types.Descriptors;

namespace eShop.Catalog.Types.Configuration;

public class UseToUpperAttribute : ObjectFieldDescriptorAttribute
{
    public UseToUpperAttribute([CallerLineNumber] int order = default)
    {
        Order = order;
    }
    
    protected override void OnConfigure(
        IDescriptorContext context, 
        IObjectFieldDescriptor descriptor, 
        MemberInfo member)
        => descriptor.UseToUpper();
}