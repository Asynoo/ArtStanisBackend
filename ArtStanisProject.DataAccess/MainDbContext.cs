using System.Data.Entity;
using ArtStanisProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ArtStanisProject.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            
        }

        public virtual Microsoft.EntityFrameworkCore.DbSet<ClientEntity> Clients { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<AddressEntity> Addresses { get; set; }
        
    }
}