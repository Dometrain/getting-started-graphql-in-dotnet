namespace Lesson2.Types;

public class User
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; init; }
    public string? PhoneNumber { get; set; }
    public string? Bio { get; set; }
    public string? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Picture? ProfilePicture { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
    public string? Locale { get; set; }
    public bool EmailVerified { get; set; }
    public List<string> UserInterests { get; set; } = new List<string>();
    public List<string> UserRoles { get; set; } = new List<string>();
    public bool IsActive { get; set; }
    public DateTime LastLogin { get; set; }
    public string? Education { get; set; }
    public string? Work { get; set; }
    public string? RelationshipStatus { get; set; }
    public string? Hometown { get; set; }
    public List<NewsStory> NewsFeed { get; set; } = new List<NewsStory>();
    public List<User> Friends { get; set; } = new List<User>();
}

[ExtendObjectType<User>]
public static class UserNode
{
    public static IEnumerable<User> GetFriends([Parent] User user, int? first)
    {
        if (first is null)
        {
            return user.Friends;
        }

        return user.Friends.Take(first.Value);
    }
}