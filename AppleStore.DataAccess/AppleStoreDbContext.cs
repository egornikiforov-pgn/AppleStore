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
        public DbSet<CartItemProductEntity> CartItemProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    } 
}
