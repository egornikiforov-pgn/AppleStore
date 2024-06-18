using AppleStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using AppleStore.DataAccess.Configurations;

namespace AppleStore.DataAccess
{
    public class AppleStoreDbContext(DbContextOptions<AppleStoreDbContext> options) :
        DbContext(options)
    {
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<ProductEntity> Products  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
