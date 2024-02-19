using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Seller;

public class SellerAddRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
