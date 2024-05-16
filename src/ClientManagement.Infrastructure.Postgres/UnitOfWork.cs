using ClientManagement.Application;

namespace ClientManagement.Infrastructure.Postgres;

public class UnitOfWork : IUnitOfWork
{
    private readonly ClientDbContext _clientDbContext;

    public UnitOfWork(ClientDbContext clientDbContext)
    {
        _clientDbContext = clientDbContext;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _clientDbContext.SaveChangesAsync(cancellationToken);
    }
}