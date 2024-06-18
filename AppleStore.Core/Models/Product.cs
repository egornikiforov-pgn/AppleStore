namespace AppleStore.Core.Models;
public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public byte[]? Image { get; } 

    public Product(Guid id, string name, 
        decimal price, byte[]? image) 
    {
        Id = id;
        Name = name;
        Price = price;
        Image = image ?? Array.Empty<byte>();;
    }
}
