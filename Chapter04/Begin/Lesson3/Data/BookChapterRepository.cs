namespace Lesson3.Data;

public class BookChapterRepository(InMemoryData data)
{
    public BookChapter? GetBookChapterById(int bookId, int chapterNumber)
        => data.BookChapters.FirstOrDefault(x => x.BookId == bookId && x.ChapterNumber == chapterNumber);
}