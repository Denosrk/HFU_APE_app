using MLZ2025.Core.Model;
using Newtonsoft.Json;

namespace MLZ2025.Core.Services;

public class HttpServerAccess : IHttpServerAccess
{
    private readonly HttpClient client;

    public HttpServerAccess(HttpClient client)
    {
        this.client = client;
        this.client.BaseAddress = new Uri("http://localhost:3000");
    }

    public override async Task<IList<ServerAddress>> GetAddressesAsync()
    {
        var response = await client.GetAsync("/addresses/");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // TODO Use System.Text.Json instead of Newtonsoft.Json
        var result = JsonConvert.DeserializeObject<IList<ServerAddress>>(content);

        if (result == null)
        {
            throw new InvalidOperationException("Could not get addresses from server.");
        }

        return result;
    }
}
