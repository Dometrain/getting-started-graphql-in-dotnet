namespace Lesson2.Types;

public class Comment
{
    public required string Id { get; set; }
    public required string Body { get; set; }
    public required User Author { get; set; }
    public DateTime Date { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsLiked { get; set; }
    public bool IsDisliked { get; set; }
}