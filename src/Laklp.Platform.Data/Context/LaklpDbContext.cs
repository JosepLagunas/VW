using GeoCoordinatePortable;
using Laklp.Platform.Data.Context.Configurations;
using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.SqlServer.Server;

namespace Laklp.Platform.Data.Context
{
    public class LaklpDbContext : DbContext
    {
        public DbSet<DocumentaryResource> DocumentaryResources { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Delegation> Delegations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TeamLead> TeamLeads { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<MaintenanceService> MaintenanceServices { get; set; }
        public DbSet<CheckPoint> CheckPoints { get; set; }

        public LaklpDbContext(DbContextOptions<LaklpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CheckpointConfiguration())
                .ApplyConfiguration(new CompanyConfiguration())
                .ApplyConfiguration(new DelegationConfiguration())
                .ApplyConfiguration(new DocumentaryResourceConfiguration())
                .ApplyConfiguration(new EmployeeConfiguration())
                .ApplyConfiguration(new GeoCoordinateConfiguration())
                .ApplyConfiguration(new InterventionConfiguration())
                .ApplyConfiguration(new MaintenanceServiceConfiguration())
                .ApplyConfiguration(new TeamLeadConfiguration())
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}