using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer.Server;

namespace Laklp.Platform.Data.Context.Configurations
{
    public class InterventionConfiguration : IEntityTypeConfiguration<Intervention>
    {
        public void Configure(EntityTypeBuilder<Intervention> builder)
        {
            builder.ToTable("Interventions", "QualityAssurance");
        }
    }
}