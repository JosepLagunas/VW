using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Server;

namespace Laklp.Platform.Data.Context.Configurations
{
    public class MaintenanceServiceConfiguration : IEntityTypeConfiguration<MaintenanceService>
    {
        public void Configure(EntityTypeBuilder<MaintenanceService> builder)
        {
            builder.ToTable("MaintenanceServices", "QualityAssurance");
        }
    }
}