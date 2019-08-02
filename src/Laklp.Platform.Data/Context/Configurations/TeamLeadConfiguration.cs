using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laklp.Platform.Data.Context.Configurations
{
    internal class TeamLeadConfiguration : IEntityTypeConfiguration<TeamLead>
    {
        public void Configure(EntityTypeBuilder<TeamLead> builder)
        {
            builder.ToTable("TeamLeads", "HumanResources");
        }
    }
}