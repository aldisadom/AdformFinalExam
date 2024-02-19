using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<int> Add(UserEntity item);
    Task<IEnumerable<UserEntity>> Get();
    Task<UserEntity?> Get(int id);
}
