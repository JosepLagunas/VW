using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Server;

namespace Laklp.Platform.Data.Context.Configurations
{
    public class CheckpointConfiguration : IEntityTypeConfiguration<CheckPoint>
    {
        public void Configure(EntityTypeBuilder<CheckPoint> builder)
        {
            builder.ToTable("CheckPoints", "QualityAssurance");
        }
    }
}