using Contracts.Responces.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Responces.Orders;

public class OrderResponce
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
    public Guid SellerId { get; set; }
    public List<ItemResponce>? Items { get; set; }
}
