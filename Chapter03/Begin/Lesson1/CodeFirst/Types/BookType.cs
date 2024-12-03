using HotChocolate.Types;

namespace AnnotationBased.Types;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(t => t.Title);
        descriptor.Field(t => t.Author);
    }
}