using HotChocolate.Language;
using HotChocolate.Types;

namespace AnnotationBased.Types;

public class QueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Field("sayHello")
            .Argument("name", a => a.Type<NonNullType<StringType>>().DefaultValue("World"))
            .Type<NonNullType<StringType>>()
            .Resolve(ctx =>
            {
                var name = ctx.ArgumentValue<string>("name");
                return $"Hello {name}.";
            });

        descriptor
            .Field("book")
            .Type<NonNullType<BookType>>()
            .Resolve(_ => new Book("C# in Depth", new Author("Jon Skeet"), DateTime.Now));
    }
}
