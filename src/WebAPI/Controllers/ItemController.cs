using Application.Interfaces;
using Contracts.Requests.Item;
using Contracts.Responces;
using Contracts.Responces.Item;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a item controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemController> _logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="itemService"></param>
    /// <param name="logger"></param>
    public ItemController(IItemService itemService, ILogger<ItemController> logger)
    {
        _itemService = itemService;
        _logger = logger;
    }

    /// <summary>
    /// Get single item
    /// </summary>
    /// <param name="id">Item ID</param>
    /// <returns>Item properties</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ItemResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _itemService.Get(id));
    }

    /// <summary>
    /// Get all items
    /// </summary>
    /// <returns>List of items</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ItemListResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok((await _itemService.Get()).items);
    }

    /// <summary>
    /// Adds item to seller
    /// </summary>
    /// <param name="item">Item data to add</param>
    /// <returns>ID of item</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ItemAddResponce), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(ItemAddRequest item)
    {
        return CreatedAtAction(nameof(Add), await _itemService.Add(item));
    }
}