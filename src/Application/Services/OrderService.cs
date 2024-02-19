using Application.Interfaces;
using Contracts.Requests.Item;
using Contracts.Requests.Orders;
using Contracts.Responces.Item;
using Contracts.Responces.Orders;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;

namespace Application.Services;


public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUserService _userService;

    public OrderService(IOrderRepository orderRepository, IItemRepository itemRepository, IUserService userService)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _userService = userService;
    }

    private async Task FillOrderWithItems(OrderResponce order)
    {
        order.Items = (await _itemRepository.GetOrderItems(order.Id)).Select(i => new ItemResponce()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price,
            SellerId = i.SellerId,
        }).ToList();
    }

    public async Task<OrderResponce> Get(Guid id)
    {
        OrderEntity orderEntity = await _orderRepository.Get(id)
            ?? throw new NotFoundException("Order not found in DB");

        OrderResponce responce = new()
        {
            Id = orderEntity.Id,
            UserId = orderEntity.UserId,
            SellerId = orderEntity.SellerId,
            Status = orderEntity.Status
        };

        await FillOrderWithItems(responce);

        return responce;
    }

    public async Task<OrderListResponce> GetUserOrders(int userId)
    {
        IEnumerable<OrderEntity> orderEntities = await _orderRepository.GetUserOrders(userId);

        OrderListResponce responce = new()
        {

            Orders = orderEntities.Select(o => new OrderResponce()
            {
                Id = o.Id,
                UserId = o.UserId,
                SellerId = o.SellerId,
                Status = o.Status
            }
            ).ToList()
        };

        foreach (OrderResponce order in responce.Orders)
        {
            await FillOrderWithItems(order);
        }

        return responce;
    }

    public async Task<OrderListResponce> Get()
    {
        IEnumerable<OrderEntity> orderEntities = await _orderRepository.Get();

        OrderListResponce responce = new()
        {

            Orders = orderEntities.Select(o => new OrderResponce()
            {
                Id = o.Id,
                UserId = o.UserId,
                SellerId = o.SellerId,
                Status = o.Status
            }
            ).ToList()
        };

        foreach (OrderResponce order in responce.Orders)
        {
            await FillOrderWithItems(order);
        }

        return responce;
    }

    public async Task<OrderAddResponce> Add(OrderAddRequest order)
    {
        UserEntity userEntity = await _userService.Get(order.UserId)
                ?? throw new ArgumentException("User not found");

        ItemEntity? itemEntity = await _itemRepository.Get(order.ItemId)
            ?? throw new ArgumentException("Sellected item not found");

        OrderEntity? orderEntity = await _orderRepository.GetUnfinishedOrder(userEntity.Id);

        Guid orderId;

        if (orderEntity is null)
        {
            orderEntity = new()
            {
                UserId = userEntity.Id,
                SellerId = itemEntity.SellerId,
                Status = OrderStatus.Ordering.ToString()
            };

            orderId = await _orderRepository.Add(orderEntity);
        }
        else
        {
            if (orderEntity.SellerId != itemEntity.SellerId)
            {
                throw new ArgumentException("Sellected item is from different seller, finish previous order");
            }

            orderId = orderEntity.Id;
        }

        OrderItemEntity orderItemEntity = new()
        {
            OrderId = orderId,
            ItemId = itemEntity.Id,
        };
        await _orderRepository.AddItemToOrder(orderItemEntity);

        OrderAddResponce responce = new()
        {
            Id = orderId
        };

        return responce;
    }

    public async Task<OrderStatusResponce> BuyOrder(Guid id)
    {
        OrderEntity orderEntity = await _orderRepository.Get(id)
            ?? throw new NotFoundException("Order not found");

        if (orderEntity.Status != OrderStatus.Ordering.ToString())
            throw new ArgumentException($"Buy is not possible for payed or delivered orders, order status is {orderEntity.Status}");

        orderEntity.Status = OrderStatus.Payed.ToString();

        orderEntity = await _orderRepository.Update(orderEntity);

        OrderStatusResponce orderStatusResponce = new()
        {
            Status = orderEntity.Status
        };

        return orderStatusResponce;
    }

    public async Task<OrderStatusResponce> DeliverOrder(Guid id)
    {
        OrderEntity orderEntity = await _orderRepository.Get(id)
            ?? throw new NotFoundException("Order not found");

        if (orderEntity.Status != OrderStatus.Payed.ToString())
            throw new ArgumentException($"Deliver is not possible for unpayed or delivered orders, order status is {orderEntity.Status}");

        orderEntity.Status = OrderStatus.Delivered.ToString();

        orderEntity = await _orderRepository.Update(orderEntity);

        OrderStatusResponce orderStatusResponce = new()
        {
            Status = orderEntity.Status
        };

        return orderStatusResponce;
    }

    public async Task<int> CleanUnfinishedOrders(DateTime date)
    {
        return await _orderRepository.DeleteOrdersByDate(date);
    }
}
