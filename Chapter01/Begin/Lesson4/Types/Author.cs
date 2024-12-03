using System.Threading;
namespace Lesson4.Types;

public record Author(string Name);

public interface IPet
{
    string Name { get; }
}

[UnionType]
public interface IMammal
{

}

public class Dog : IPet, IMammal
{
    public Dog(string name, string breed)
    {
        Name = name;
        Breed = breed;
    }

    public string Name { get; }

    public string Breed { get; }
}

public class Cat : IPet, IMammal
{
    public Cat(string name, CatLives lives)
    {
        Name = name;
        Lives = lives;
    }

    public string Name { get; }

    public CatLives Lives { get; }
}

public class Parrot : IPet
{
    public Parrot(string name, bool canTalk)
    {
        Name = name;
        CanTalk = canTalk;
    }

    public string Name { get; }


    public bool CanTalk { get; }
}

public enum CatLives { One, Two, Three, Four, Five, Six, Seven, Eight, Nine }