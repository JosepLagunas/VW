using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laklp.Platform.Data.Context.Configurations
{
    internal class DocumentaryResourceConfiguration : IEntityTypeConfiguration<DocumentaryResource>
    {
        public void Configure(EntityTypeBuilder<DocumentaryResource> builder)
        {
            builder.ToTable("DocumentaryResources", "Documentation");
        }
    }
}