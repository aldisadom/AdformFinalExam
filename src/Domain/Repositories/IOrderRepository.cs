using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository
{
    Task<Guid> Add(OrderEntity item);
    Task Delete(Guid id);
    Task<IEnumerable<OrderEntity>> Get();
    Task<OrderEntity?> Get(Guid id);
    Task Update(OrderEntity item);
}
