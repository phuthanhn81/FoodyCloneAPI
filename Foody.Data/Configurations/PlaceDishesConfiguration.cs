using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Data.Configurations
{
    public class PlaceDishesConfiguration : IEntityTypeConfiguration<PlaceDishes>
    {
        public void Configure(EntityTypeBuilder<PlaceDishes> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(500);

            builder.Property(e => e.PhotoDish)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
