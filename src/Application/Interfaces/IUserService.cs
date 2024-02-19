using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserEntity?> Get(int id);
}
