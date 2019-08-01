using Laklp.Platform.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Laklp.Platform.Data.Context.Configurations
{
    internal class GeoCoordinateConfiguration : IEntityTypeConfiguration<Geocoordinate>
    {
        public void Configure(EntityTypeBuilder<Geocoordinate> builder)
        {
            builder.ToTable("Geocoordinates", "Miscellaneous");
        }
    }
}