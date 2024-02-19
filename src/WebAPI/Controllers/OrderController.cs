using Application.Interfaces;
using Contracts.Requests.Item;
using Contracts.Requests.Orders;
using Contracts.Responces;
using Contracts.Responces.Item;
using Contracts.Responces.Orders;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a order controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="logger"></param>
    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    /// <summary>
    /// Get all user orders order
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User orders properties</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(OrderResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int userId)
    {
        return Ok((await _orderService.GetUserOrders(userId)).Orders);
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    /// <returns>List of orders</returns>
    [HttpGet]
    [ProducesResponseType(typeof(OrderListResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok((await _orderService.Get()).Orders);
    }

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="order">Item data to add</param>
    /// <returns>ID of item</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderAddResponce), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(OrderAddRequest order)
    {
        return CreatedAtAction(nameof(Add), await _orderService.Add(order));
    }

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="orderId">Order Id to buy</param>
    /// <returns>Order status</returns>
    [HttpPut("{orderId}/buy")]
    [ProducesResponseType(typeof(OrderStatusResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> BuyOrder(Guid orderId)
    {
        return CreatedAtAction(nameof(BuyOrder), await _orderService.BuyOrder(orderId));
    }

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="orderId">Order Id to buy</param>
    /// <returns>Order status</returns>
    [HttpPut("{orderId}/deliver")]
    [ProducesResponseType(typeof(OrderStatusResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeliverOrder(Guid orderId)
    {
        return CreatedAtAction(nameof(DeliverOrder), await _orderService.DeliverOrder(orderId));
    }
}