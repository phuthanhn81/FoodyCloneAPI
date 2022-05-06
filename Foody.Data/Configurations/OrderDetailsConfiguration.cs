using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Data.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.HasKey(e => new { e.OrderID, e.DishID });

            builder.Property(e => e.Name).HasMaxLength(500);

            builder.Property(e => e.Total).HasColumnType("decimal(18,0)");
        }
    }
}
