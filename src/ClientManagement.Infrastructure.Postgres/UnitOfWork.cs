using ClientManagement.Application;

namespace ClientManagement.Infrastructure.Postgres;

public class UnitOfWork : IUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}