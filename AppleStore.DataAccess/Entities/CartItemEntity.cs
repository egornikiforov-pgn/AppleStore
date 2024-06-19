namespace AppleStore.DataAccess.Entities;
public class CartItemEntity
{
    public Guid Id { get; set; }
    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}