using Application.Interfaces;
using Contracts.Requests.Seller;
using Contracts.Responces;
using Contracts.Responces.Seller;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a seller controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;
    private readonly ILogger<SellerController> _logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="sellerService"></param>
    /// <param name="logger"></param>
    public SellerController(ISellerService sellerService, ILogger<SellerController> logger)
    {
        _sellerService = sellerService;
        _logger = logger;
    }

    /// <summary>
    /// Get single seller
    /// </summary>
    /// <param name="id">Seller ID</param>
    /// <returns>Seller properties</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SellerAddResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _sellerService.Get(id));
    }

    /// <summary>
    /// Get all sellers
    /// </summary>
    /// <returns>List of sellers</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SellerListResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok((await _sellerService.Get()).sellers);
    }

    /// <summary>
    /// Adds seller
    /// </summary>
    /// <param name="seller">Seller data to add</param>
    /// <returns>ID of seller</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SellerAddResponce), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(SellerAddRequest seller)
    {
        return CreatedAtAction(nameof(Add), await _sellerService.Add(seller));
    }
}