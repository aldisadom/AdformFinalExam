namespace Contracts.Requests.Orders;

public class OrderAddRequest
{
    public int UserId { get; set; }
    public Guid ItemId { get; set; }
}
