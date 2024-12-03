namespace Lesson3.Types;

[ObjectType<BookChapter>]
public static partial class BookChapterNode
{
    static partial void Configure(IObjectTypeDescriptor<BookChapter> descriptor)
        => descriptor.Ignore(t => t.BookId);

    [ID]
    public static BookChapterId GetId([Parent] BookChapter chapter)
        => new BookChapterId(chapter.BookId, chapter.ChapterNumber);


    [NodeResolver]
    public static BookChapter? GetChapterById(BookChapterId id, BookChapterRepository repository)
        => repository.GetBookChapterById(id.BookId, id.ChapterNumber);
}

public readonly record struct BookChapterId(int BookId, int ChapterNumber);

public class BookChapterIdSerializer : CompositeNodeIdValueSerializer<BookChapterId>
{
    protected override NodeIdFormatterResult Format(Span<byte> buffer, BookChapterId value, out int written)
    {
        if (value == default)
        {
            written = 0;
            return NodeIdFormatterResult.InvalidValue;
        }

        if (TryFormatIdPart(buffer, value.BookId, out var bookIdWritten) &&
            TryFormatIdPart(buffer[bookIdWritten..], value.ChapterNumber, out var chapterNumberWritten))
        {
            written = bookIdWritten + chapterNumberWritten;
            return NodeIdFormatterResult.Success;
        }

        written = 0;
        return NodeIdFormatterResult.BufferTooSmall;
    }

    protected override bool TryParse(ReadOnlySpan<byte> buffer, out BookChapterId value)
    {
        if (TryParseIdPart(buffer, out int bookId, out var consumed) &&
            TryParseIdPart(buffer[consumed..], out int chapterNumber, out _))
        {
            value = new BookChapterId(bookId, chapterNumber);
            return true;
        }

        value = default;
        return false;
    }
}