namespace Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
    public Guid SellerId { get; set; }
    public DateTime CreateDate { get; set; }
}
