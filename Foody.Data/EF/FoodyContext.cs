using Foody.Data.Configurations;
using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Foody.Data.EF
{
    public partial class FoodyContext : DbContext
    {
        public FoodyContext(DbContextOptions<FoodyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Place> Place { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaceConfiguration());
        }
    }
}
