using Application.Interfaces;
using Contracts.Requests.Item;
using Contracts.Responces;
using Contracts.Responces.Item;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a order controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
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
    /// <param name="user_id">User ID</param>
    /// <returns>User orders properties</returns>
    [HttpGet("{user_id}")]
    [ProducesResponseType(typeof(ItemResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int user_id)
    {
        return Ok();
//        return Ok(await _orderService.Get(user_id));
    }

    /// <summary>
    /// Get all orders
    /// </summary>
    /// <returns>List of orders</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ItemListResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok();
//        return Ok((await _orderService.Get()).items);
    }

    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="item">Item data to add</param>
    /// <returns>ID of item</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ItemAddResponce), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(ItemAddRequest item)
    {
        return Ok();
//        return CreatedAtAction(nameof(Add), await _orderService.Add(item));
    }
}