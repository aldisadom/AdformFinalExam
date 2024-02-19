using Contracts.Responces.Item;

namespace Contracts.Responces.Orders;

public class OrderResponce
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
    public Guid SellerId { get; set; }
    public List<ItemResponce>? Items { get; set; }
}
