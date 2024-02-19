namespace Domain.Entities;

public class OrderItemEntity
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
}
