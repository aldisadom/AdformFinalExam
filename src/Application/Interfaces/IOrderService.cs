using Contracts.Requests.Orders;
using Contracts.Responces.Orders;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<OrderAddResponce> Add(OrderAddRequest order);
    Task<OrderStatusResponce> BuyOrder(Guid id);
    Task<OrderStatusResponce> DeliverOrder(Guid id);
    Task<OrderListResponce> Get();
    Task<OrderResponce> Get(Guid id);
    Task<OrderListResponce> GetUserOrders(int userId);
}
