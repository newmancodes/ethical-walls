using ClientManagement.Application;
using ClientManagement.Domain;

namespace ClientManagement.Infrastructure.Postgres;

public class ClientRepository : IClientRepository
{
    private readonly ClientDbContext _clientDbContext;
    
    public ClientRepository(ClientDbContext clientDbContext)
    {
        _clientDbContext = clientDbContext;
    }

    public void Create(Client client)
    {
        _clientDbContext.Clients.Add(client);
    }
}