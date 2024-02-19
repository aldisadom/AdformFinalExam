using Clients;
using Clients.Clients;
using Domain.Clients;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Infrastructure.Clients;

public class JsonplaceholderClient : IUserClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey = string.Empty;

    public JsonplaceholderClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
    }

    //https://jsonplaceholder.typicode.com/users?id=1
    public async Task<JsonplaceholderClientDataResponse> Get(int userId)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"https://jsonplaceholder.typicode.com/users?id={userId}");

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return new JsonplaceholderClientDataResponse
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null,
            };

        List<JsonplaceholderClientData> userList = JsonConvert.DeserializeObject<IEnumerable<JsonplaceholderClientData>>(responseBody)!.ToList();

        if (userList.Count() !=1)
            return new JsonplaceholderClientDataResponse
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null,
            };

        return new JsonplaceholderClientDataResponse
        {
            IsSuccessful = true,
            ErrorMessage = null,
            Data = userList[0],
        };
    }
}