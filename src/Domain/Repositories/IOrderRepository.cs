using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository
{
    Task<Guid> Add(OrderEntity order);
    Task AddItemToOrder(OrderItemEntity order);
    Task Delete(Guid id);
    Task<IEnumerable<OrderEntity>> Get();
    Task<OrderEntity?> Get(Guid id);
    Task<OrderEntity?> GetUnfinishedOrder(int userId);
    Task<IEnumerable<OrderEntity>> GetUserOrders(int userId);
    Task<OrderEntity> Update(OrderEntity order);
}
