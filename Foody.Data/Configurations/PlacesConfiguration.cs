using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Data.Configurations
{
    public class PlacesConfiguration : IEntityTypeConfiguration<Places>
    {
        public void Configure(EntityTypeBuilder<Places> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Address).HasMaxLength(500);

            builder.Property(e => e.Name).HasMaxLength(500);

            builder.Property(e => e.PhotoUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Phone).HasMaxLength(50);
        }
    }
}
