namespace AppleStore.DataAccess.Entities;
public class CartItemEntity
{
    public Guid Id { get; set; }
    public ICollection<CartItemProductEntity> CartItemProducts { get; set; }
}