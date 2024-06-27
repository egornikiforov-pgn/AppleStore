namespace AppleStore.Ui.Models;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public byte[]? Image { get; set; }
    public int Count { get; set; } = 1;


}
