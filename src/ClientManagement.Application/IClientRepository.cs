using ClientManagement.Domain;

namespace ClientManagement.Application;

public interface IClientRepository
{
    void Create(Client client);
}