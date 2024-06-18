using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppleStore.DataAccess.Entities;

namespace AppleStore.DataAccess.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder
                .HasMany(ci => ci.Products)
                .WithMany(p => p.CartItems);
        }
    }
}