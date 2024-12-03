namespace Lesson3.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public Author Author { get; set; } = default!;
    public List<BookChapter> Chapters { get; } = new();
}