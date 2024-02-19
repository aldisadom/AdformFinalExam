using Application.Interfaces;
using Clients;
using Contracts.Requests.Item;
using Contracts.Responces.Item;
using Domain.Clients;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserClient _client;

    public UserService(IUserRepository userRepository, IUserClient client)
    {
        _userRepository = userRepository;
        _client = client;
    }

    public async Task<UserEntity?> Get(int id)
    {
        UserEntity? user = await _userRepository.Get(id);

        if (user is null)
        {
            JsonplaceholderClientDataResponse response = await _client.Get(id);

            if (!response.IsSuccessful || response.Data == null)
                throw new ClientAPIException($"Failed to get user with {id} from external API");

            user = new()
            {
                Id = response.Data.Id,
                Email = response.Data.Email,
                Name = response.Data.Name
            };

            await _userRepository.Add(user);
        }

        return user;
    }
}
