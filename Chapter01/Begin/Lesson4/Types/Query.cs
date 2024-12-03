namespace Lesson4.Types;

[QueryType]
public static class Query
{
    public static Book GetBook()
        => new Book("C# in depth.", new Author("Jon Skeet"));

    public static IReadOnlyList<IPet> GetPets(PetRepository repository)
        => repository.GetPets();
}

[MutationType]
public static class Mutation
{
    public static int AddCat(Cat cat, PetRepository repository)
    {
        repository.AddPet(cat);
        return repository.GetPets().Count;
    }

    public static int AddPet(PetInput input, PetRepository repository)
    {
        if (input.Cat is not null)
        {
            repository.AddPet(input.Cat);
        }
        else if(input.Dog is not null)
        {
            repository.AddPet(input.Dog);
        }
        else if(input.Parrot is not null)
        {
            repository.AddPet(input.Parrot);
        }
        else
        {
            throw new Exception("Invalid pet type");
        }

        return repository.GetPets().Count;
    }
}

[OneOf] public record PetInput(Dog? Dog, Cat? Cat, Parrot? Parrot);

public class PetRepository
{

    private readonly List<IPet> _pets = new();

    public PetRepository()
    {
        _pets.Add(new Dog("Buddy", "Golden Retriever"));
        _pets.Add(new Cat("Snowball", CatLives.Nine));
        _pets.Add(new Parrot("Iago", true));
    }

    public IReadOnlyList<IPet> GetPets() => _pets;

    public void AddPet(IPet pet) => _pets.Add(pet);
}
