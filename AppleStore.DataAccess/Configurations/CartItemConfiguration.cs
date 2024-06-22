using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppleStore.DataAccess.Entities;

namespace AppleStore.DataAccess.Configurations
{
    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            // Определяем связь один ко многим с таблицей-связкой CartItemProductEntity
            builder.HasMany(ci => ci.CartItemProducts)
                .WithOne(cp => cp.CartItem)
                .HasForeignKey(cp => cp.CartItemId)
                .IsRequired();
        }
    }
}