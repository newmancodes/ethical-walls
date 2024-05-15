using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Postgres;

public sealed class ClientDbContext : DbContext
{
    public ClientDbContext(DbContextOptions<ClientDbContext> options)
        : base(options)
    {
        
    }
}