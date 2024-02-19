using Domain.Entities;

namespace Domain.Repositories;

public interface ISellerRepository
{
    Task<SellerEntity?> Get(Guid id);
    Task<IEnumerable<SellerEntity>> Get();
    Task<Guid> Add(SellerEntity item);
}
