using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class OrderItemEntity
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
}
