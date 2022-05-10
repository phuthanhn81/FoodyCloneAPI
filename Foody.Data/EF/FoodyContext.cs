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

        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<PlaceDishes> PlaceDishes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlacesConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceDishesConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }
    }
}
