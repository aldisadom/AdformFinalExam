using Clients.Clients;

namespace Domain.Clients;

public class JsonplaceholderClientDataResponse
{
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public JsonplaceholderClientData? Data { get; set; }
}