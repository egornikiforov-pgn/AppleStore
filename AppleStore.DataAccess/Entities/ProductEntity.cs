using AppleStore.DataAccess.Entities;

namespace AppleStore.DataAccess.Entities;
public class ProductEntity
{
    public Guid Id { get; set;}
    public string? Name { get; set;}
    public decimal Price { get; set;}
    public byte[] Image {get;set;} = Array.Empty<byte>();
    public ICollection<CartItemProductEntity> CartItemProducts { get; set; }
}