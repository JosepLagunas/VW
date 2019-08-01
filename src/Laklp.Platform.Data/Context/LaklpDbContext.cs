using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laklp.Platform.Data.Context
{
    public class LaklpDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Resource> DocumentaryResources { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        
        public LaklpDbContext(DbContextOptions<LaklpDbContext> options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}