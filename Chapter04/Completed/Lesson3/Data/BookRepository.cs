namespace Lesson3.Data;

public class BookRepository(InMemoryData data)
{
    public Book? GetBookById(int id)
        => data.Books.FirstOrDefault(x => x.Id == id);
    
    public IEnumerable<Book> GetAllBooks()
        => data.Books;
}