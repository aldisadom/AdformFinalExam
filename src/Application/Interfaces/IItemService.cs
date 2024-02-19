using Contracts.Requests.Item;
using Contracts.Responces.Item;

namespace Application.Interfaces;

public interface IItemService
{
    Task<ItemAddResponce> Add(ItemAddRequest item);
    Task<ItemResponce> Get(Guid id);
    Task<ItemListResponce> Get();
}