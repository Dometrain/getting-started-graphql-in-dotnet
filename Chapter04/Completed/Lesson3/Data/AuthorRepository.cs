namespace Lesson3.Data;

public class AuthorRepository(InMemoryData data)
{
    public Author? GetAuthorById(int id)
        => data.Authors.FirstOrDefault(x => x.Id == id);
    
    public IEnumerable<Author> GetAllAuthors()
        => data.Authors;
}