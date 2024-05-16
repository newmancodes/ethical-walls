namespace ClientManagement.Domain;

public class Client
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Client() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Client(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Client Create(string name)
    {
        var client = new Client(Guid.NewGuid(), name);
        DomainEvents.Raise(new ClientCreatedDomainEvent(client));

        return client;
    }
}