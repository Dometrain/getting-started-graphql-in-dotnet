namespace AnnotationBased.Types;

public class Query
{
    public string SayHello(string name = "World")
        => $"Hello {name}.";

    public Book GetBook()
        => new Book("C# in Depth", new Author("Jon Skeet"));
}
