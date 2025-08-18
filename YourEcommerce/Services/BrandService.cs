using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class BrandService : IBrandService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BrandService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<BrandDto>> GetAll()
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync("api/brands");

        if (!response.IsSuccessStatusCode) return new();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var content = await response.Content.ReadAsStringAsync();
        var brands = JsonSerializer.Deserialize<List<BrandDto>>(content, options);

        return brands ?? new List<BrandDto>();
    }

    public async Task<IEnumerable<BrandDto>> GetAllFlat()
    {
        var brands = await GetAll();

        return brands.Select(b => new BrandDto
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description,
            BrandImage = b.BrandImage
        });
    }

    public async Task<BrandDto?> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync($"api/brands/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var brand = JsonSerializer.Deserialize<BrandDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return brand;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/brands/{id}");

        return response.IsSuccessStatusCode;
    }
}