using Application.Interfaces;
using Contracts.Requests.Seller;
using Contracts.Responces.Item;
using Contracts.Responces.Seller;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.Services;

public class SellerService : ISellerService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<SellerResponce> Get(Guid id)
    {
        SellerEntity sellerEntity = await _sellerRepository.Get(id)
            ?? throw new NotFoundException("Seller not found in DB");

        SellerResponce responce = new()
        {
            Id = id,
            Name = sellerEntity.Name
        };

        return responce;
    }

    public async Task<SellerListResponce> Get()
    {
        IEnumerable<SellerEntity> sellerEntities = await _sellerRepository.Get();

        SellerListResponce responce = new ()
        {
            sellers = sellerEntities.Select(i => new SellerResponce()
            {
                Id = i.Id,
                Name = i.Name
            }
            ).ToList()
        };

        return responce;
    }

    public async Task<SellerAddResponce> Add(SellerAddRequest seller)
    {
        SellerEntity sellerEntity = new()
        {
            Name = seller.Name
        };

        Guid id = await _sellerRepository.Add(sellerEntity);

        SellerAddResponce responce = new()
        {
            Id = id
        };

        return responce;
    }
}
