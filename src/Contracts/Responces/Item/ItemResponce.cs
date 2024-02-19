namespace Contracts.Responces.Item;

public class ItemResponce
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid SellerId { get; set; }
}
