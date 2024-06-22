using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppleStore.DataAccess.Entities;

namespace AppleStore.DataAccess.Configurations
{
    public class CartItemProductEntityConfiguration : IEntityTypeConfiguration<CartItemProductEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemProductEntity> builder)
        {
            builder.ToTable("CartItemProduct");

            builder.HasKey(cp => new { cp.CartItemId, cp.ProductId });

            // Определяем связь многие ко многим между CartItemEntity и ProductEntity
            builder.HasOne(cp => cp.CartItem)
                .WithMany(ci => ci.CartItemProducts)
                .HasForeignKey(cp => cp.CartItemId)
                .IsRequired();

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CartItemProducts)
                .HasForeignKey(cp => cp.ProductId)
                .IsRequired();
        }
    }
}