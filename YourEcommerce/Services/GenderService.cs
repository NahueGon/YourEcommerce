using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class GenderService : IGenderService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GenderService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<GenderDto>> GetAll()
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync("api/genders");

        if (!response.IsSuccessStatusCode) return new();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var content = await response.Content.ReadAsStringAsync();
        var genders = JsonSerializer.Deserialize<List<GenderDto>>(content, options);

        return genders ?? new List<GenderDto>();
    }

    public async Task<IEnumerable<GenderDto>> GetAllFlat()
    {
        var genders = await GetAll();

        return genders.Select(g => new GenderDto
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            GenderImage = g.GenderImage
        });
    }

    public async Task<GenderDto?> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync($"api/genders/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var gender = JsonSerializer.Deserialize<GenderDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return gender;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/genders/{id}");

        return response.IsSuccessStatusCode;
    }
}