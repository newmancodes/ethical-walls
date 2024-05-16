using ClientManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Postgres;

public class ClientDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    
    public ClientDbContext(DbContextOptions<ClientDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
    }
}