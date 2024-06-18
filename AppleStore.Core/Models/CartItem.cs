namespace AppleStore.Core.Models;
public class CartItem
{
    public Guid Id { get;}
    public ICollection<Product>? Products { get; }
    
    public CartItem(Guid id, List<Product> products)
    {
        Id = id;
        Products = products;
    }
}
