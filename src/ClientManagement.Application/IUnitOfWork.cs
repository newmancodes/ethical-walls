namespace ClientManagement.Application;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}