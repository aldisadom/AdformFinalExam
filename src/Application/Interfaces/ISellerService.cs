using Contracts.Requests.Seller;
using Contracts.Responces.Seller;

namespace Application.Interfaces;

public interface ISellerService
{
    Task<SellerAddResponce> Add(SellerAddRequest item);
    Task<SellerResponce> Get(Guid id);
    Task<SellerListResponce> Get();
}
