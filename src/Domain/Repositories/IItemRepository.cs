using Domain.Entities;

namespace Domain.Interfaces;

public interface IItemRepository
{
    Task<ItemEntity?> Get(Guid id);
    Task<IEnumerable<ItemEntity>> GetOrderItems(Guid orderId);
    Task<IEnumerable<ItemEntity>> Get();
    Task<Guid> Add(ItemEntity item);
}