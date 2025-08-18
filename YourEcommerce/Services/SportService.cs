using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class SportService : ISportService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SportService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<SportDto>> GetAll()
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync("api/sports");

        if (!response.IsSuccessStatusCode) return new();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var content = await response.Content.ReadAsStringAsync();
        var sports = JsonSerializer.Deserialize<List<SportDto>>(content, options);

        return sports ?? new List<SportDto>();
    }

    public async Task<IEnumerable<SportDto>> GetAllFlat()
    {
        var sports = await GetAll();

        return sports.Select(s => new SportDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            SportImage = s.SportImage
        });
    }

    public async Task<SportDto?> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync($"api/sports/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var sport = JsonSerializer.Deserialize<SportDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return sport;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/sports/{id}");

        return response.IsSuccessStatusCode;
    }
}