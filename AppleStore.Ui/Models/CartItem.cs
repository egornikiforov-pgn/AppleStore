namespace AppleStore.Ui.Models;
public class CartItem
{
    public Guid Id { get; set; }
    public ICollection<Product>? Products { get; set; }
    
}
