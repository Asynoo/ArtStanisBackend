using ArtStanisProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtStanisProject.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            
        }

        public virtual DbSet<ClientEntity> Clients { get; set; }
    }
}