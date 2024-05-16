namespace ClientManagement.Domain;

public record ClientCreatedDomainEvent : IDomainEvent
{
    public Guid EventId { get; init; }
    
    public Guid ClientId { get; init; }
    
    public DateTime OccurredAt { get; init; }
    
    public ClientCreatedDomainEvent(Client client)
    {
        EventId = Guid.NewGuid();
        ClientId = client.Id;
        OccurredAt = DateTime.UtcNow;
    }
}