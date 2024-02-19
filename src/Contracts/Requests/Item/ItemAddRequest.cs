using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Item;

public class ItemAddRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public Guid SellerId { get; set; }
}
