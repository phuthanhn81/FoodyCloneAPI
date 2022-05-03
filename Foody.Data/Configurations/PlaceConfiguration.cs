using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Data.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Address).HasMaxLength(500);

            builder.Property(e => e.Name).HasMaxLength(500);

            builder.Property(e => e.PhotoUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
