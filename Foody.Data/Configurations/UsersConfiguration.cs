using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(e => new { e.ID });
            builder.Property(e => e.UserName).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.Password).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.RefreshToken).HasMaxLength(500).IsUnicode(false);
            builder.Property(e => e.Email).HasMaxLength(500).IsUnicode(false);
        }
    }
}
