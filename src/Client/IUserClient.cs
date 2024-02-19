using Domain.Clients;

namespace Clients;

public interface IUserClient
{
    Task<JsonplaceholderClientDataResponse> Get(int userId);
}