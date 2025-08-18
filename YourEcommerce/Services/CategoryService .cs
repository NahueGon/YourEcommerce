using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync("api/categories");

        if (!response.IsSuccessStatusCode) return new();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(content, options);

        return categories ?? new List<CategoryDto>();
    }

    public async Task<IEnumerable<CategoryDto>> GetAllFlat()
    {
        var categories = await GetAll();

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CategoryImage = c.CategoryImage
        });
    }

    public async Task<CategoryDto?> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync($"api/categories/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var category = JsonSerializer.Deserialize<CategoryDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return category;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/categories/{id}");

        return response.IsSuccessStatusCode;
    }
}