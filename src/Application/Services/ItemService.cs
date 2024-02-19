using Application.Interfaces;
using Contracts.Requests.Item;
using Contracts.Responces.Item;
using Contracts.Responces.Seller;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;

namespace Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ISellerRepository _sellerRepository;

    public ItemService(IItemRepository itemRepository, ISellerRepository sellerRepository)
    {
        _itemRepository = itemRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<ItemResponce> Get(Guid id)
    {
        ItemEntity item = await _itemRepository.Get(id)
            ?? throw new NotFoundException("Item not found in DB");

        ItemResponce responce = new()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            SellerId = item.SellerId,
        };

        return responce;
    }

    public async Task<ItemListResponce> Get()
    {
        IEnumerable<ItemEntity> itemEntities = await _itemRepository.Get();

        ItemListResponce responce = new ()
        {
            items = itemEntities.Select(i => new ItemResponce()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                SellerId = i.SellerId,
            }
            ).ToList()
        };
        
        return responce;
    }

    public async Task<ItemAddResponce> Add(ItemAddRequest item)
    {
        _ = (await _sellerRepository.Get(item.SellerId))
                ?? throw new ArgumentException("Invalid seller ID");

        ItemEntity itemEntity = new()
        {
            Name = item.Name,
            Price = item.Price,
            SellerId = item.SellerId
        };

        Guid id = await _itemRepository.Add(itemEntity);

        ItemAddResponce responce = new()
        {
            Id = id
        };

        return responce;
    }
}