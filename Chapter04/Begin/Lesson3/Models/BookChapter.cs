namespace Lesson3.Models;

public class BookChapter
{
    public int BookId { get; set; }
    public int ChapterNumber { get; set; }
    public string Title { get; set; } = default!;
    public int PageCount { get; set; }
    public Book Book { get; set; } = default!;
}