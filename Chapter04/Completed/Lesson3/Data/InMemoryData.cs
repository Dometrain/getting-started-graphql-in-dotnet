namespace Lesson3.Data;

public class InMemoryData
{
    public InMemoryData()
    {
        var author1 = new Author { Id = 1, Name = "J.R.R. Tolkien" };
        var author2 = new Author { Id = 2, Name = "George R.R. Martin" };
        
        var book1 = new Book
        {
            Id = 1,
            Title = "The Fellowship of the Ring",
            Author = author1,
        };
        
        book1.Chapters.Add(new BookChapter
        {
            BookId = 1, 
            ChapterNumber = 1, 
            Title = "A Long-expected Party", 
            PageCount = 30, 
            Book = book1
        });
        
        book1.Chapters.Add(new BookChapter
        {
            BookId = 1, 
            ChapterNumber = 2, 
            Title = "The Shadow of the Past", 
            PageCount = 25, 
            Book = book1
        });

        var book2 = new Book
        {
            Id = 2,
            Title = "A Game of Thrones",
            Author = author2,
        };
        
        book2.Chapters.Add(new BookChapter
        {
            BookId = 2, 
            ChapterNumber = 1, 
            Title = "Bran", 
            PageCount = 20, 
            Book = book2
        });
        
        book2.Chapters.Add(new BookChapter
        {
            BookId = 2, 
            ChapterNumber = 2, 
            Title = "Catelyn", 
            PageCount = 18, 
            Book = book2
        });
        
        Books.AddRange([book1, book2]);
        Authors.AddRange([author1, author2]);
        BookChapters.AddRange(book1.Chapters);
        BookChapters.AddRange(book2.Chapters);
    }

    public List<Book> Books { get; } = new();

    public List<Author> Authors { get; } = new();

    public List<BookChapter> BookChapters { get; } = new();
}