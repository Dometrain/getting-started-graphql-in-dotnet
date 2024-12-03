namespace Lesson2.Types;

[QueryType]
public static class Query
{
    public static User GetMe()
    {
        List<string> cheechAndChongQuotes = new List<string>
        {
            "Hey man, am I driving okay?",
            "I think we're parked, man.",
            "I been smoking since I was born, man, I can smoke anything, man.",
            "Is that a joint man? I got that, looks like a quarter pounder man.",
            "Oh wow man! Quickly, I think I just smoked a Labrador.",
            "Hey how am I driving, man? I think we're parked."
        };

        List<string> starWarsQuotes = new List<string>
        {
            "Help me, man. You're my only hope.",
            "The stuff will be with you. Always.",
            "Never tell me the odds.",
            "Do. Or do not. There is no try.",
            "I've got a bad feeling about this.",
            "It's a trap!",
            "So this is how liberty dies…with thunderous applause.",
            "The things I do for love.",
            "Chewie, we're home.",
            "I am one with the Force. The Force is with me.",
            "It's not wise to upset a Wookiee.",
            "I find your lack of faith disturbing."
        };

        User woody = new User
        {
            Id = "5",
            Name = "woody",
            FirstName = "Woody",
            LastName = "Cowboy",
            Email = "woody@icloud.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "woody.jpg" }
        };

        User buzz = new User
        {
            Id = "6",
            Name = "buzz",
            FirstName = "Buzz",
            LastName = "Lightyear",
            Email = "buzz@amazon.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "buzz.jpg" }
        };

        User elsa = new User
        {
            Id = "7",
            Name = "elsa",
            FirstName = "Elsa",
            LastName = "Snow",
            Email = "its@cold.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "elsa.jpg" }
        };

        User anna = new User
        {
            Id = "8",
            Name = "anna",
            FirstName = "Anna",
            LastName = "Summer",
            Email = "anna@hotmail.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "anna.jpg" }
        };

        User mary = new User
        {
            Id = "3",
            Name = "mary",
            FirstName = "Mary",
            LastName = "Elephant",
            Email = "elephant@inthe.room",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "mary.jpg" },
            Friends = new List<User> { elsa, anna },
        };

        mary.NewsFeed = new List<NewsStory> {
                new NewsStory
                {
                    Id = "7",
                    Title = "Mary's Mindfulness Moments",
                    Body = "I find peace in the quiet of the forest.",
                    Author = mary,
                    Date = DateTime.Now.AddDays(-1),
                    Image = new Picture { Width = 1000, Height = 600, Url = "forest.jpg" },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = "13",
                            Body = "So serene, I love it!",
                            Author = mary,
                            Date = DateTime.Now
                        }
                    }
                }
            };

        User chong = new User
        {
            Id = "2",
            Name = "chong",
            FirstName = "Tommy",
            LastName = "Chong",
            Email = "chong@chang.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "chong.jpg" },
            Friends = new List<User> { buzz, woody }
        };

        User jane = new User
        {
            Id = "4",
            Name = "jane",
            FirstName = "Jane",
            LastName = "Doe",
            Email = "doe@doe.co.uk",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "jane.jpg" },
            Friends = new List<User> { elsa, buzz }
        };

        User cheech = new User
        {
            Id = "1",
            Name = "cheech",
            FirstName = "Cheech",
            LastName = "Marin",
            Email = "cheech@smoke.com",
            ProfilePicture = new Picture { Width = 500, Height = 500, Url = "cheech.jpg" },
            Friends = new List<User> { chong, mary, jane, woody, elsa }
        };

        woody.Friends = new List<User> { buzz, cheech };
        buzz.Friends = new List<User> { woody, chong };
        elsa.Friends = new List<User> { anna, mary };
        anna.Friends = new List<User> { elsa, jane };

        for (int i = 0; i < 6; i++)
        {
            NewsStory story = new NewsStory
            {
                Id = (i + 1).ToString(),
                Title = $"New Movie Adventure {i + 1}",
                Body = cheechAndChongQuotes[i],
                Author = cheech,
                Date = DateTime.Now,
                Image = new Picture { Width = 1000, Height = 600, Url = $"tijuana{i + 1}.jpg" },
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = (i * 2 + 1).ToString(),
                        Body = starWarsQuotes[i * 2],
                        Author = chong,
                        Date = DateTime.Now
                    },
                    new Comment
                    {
                        Id = (i * 2 + 2).ToString(),
                        Body = starWarsQuotes[i * 2 + 1],
                        Author = chong,
                        Date = DateTime.Now
                    }
                }
            };

            cheech.NewsFeed.Add(story);
            chong.NewsFeed.Add(story);
            woody.NewsFeed.Add(story);
            buzz.NewsFeed.Add(story);
            elsa.NewsFeed.Add(story);
            anna.NewsFeed.Add(story);
        }

        return cheech;
    }
}
