using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppleStore.DataAccess.Entities;

namespace AppleStore.DataAccess.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            // Определяем связь один ко многим с таблицей-связкой CartItemProductEntity
            builder.HasMany(p => p.CartItemProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId)
                .IsRequired();
        }
    }
}