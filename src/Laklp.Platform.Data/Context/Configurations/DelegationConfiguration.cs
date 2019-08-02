using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laklp.Platform.Data.Context.Configurations
{
    internal class DelegationConfiguration : IEntityTypeConfiguration<Delegation>
    {
        public void Configure(EntityTypeBuilder<Delegation> builder)
        {
            builder.ToTable("Delegations", "Places");
        }
    }
}