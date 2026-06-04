using System.Text.Json;
using CurrencyApp.API.Dtos;

namespace CurrencyApp.API.Services;

public class NbpService
{
    private readonly HttpClient _httpClient;

    public NbpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<NbpResponse>?> GetRatesAsync()
    {
        var json = await _httpClient.GetStringAsync(
            "https://api.nbp.pl/api/exchangerates/tables/A/?format=json");

        return JsonSerializer.Deserialize<List<NbpResponse>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }
}