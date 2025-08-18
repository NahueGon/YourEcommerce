using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<ProductViewModel>> GetAll()
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.GetAsync("api/products");

        if (!response.IsSuccessStatusCode) return new List<ProductViewModel>();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
        var content = await response.Content.ReadAsStringAsync();

        var products = JsonSerializer.Deserialize<List<ProductViewModel>>(content, options);

        return products ?? new List<ProductViewModel>();
    }

    public async Task<IEnumerable<ProductDto>> GetAllFlat()
    {
        var products = await GetAll() ?? new List<ProductViewModel>();

        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            TotalStock = p.TotalStock,
            Category = p.Category?.Name,
            Sport = p.Sport?.Name,
            Brand = p.Brand?.Name,
            Gender = p.Gender?.Name,
            ProductTags = string.Join(", ", p.ProductTags.Select(t => t.Tag.Name))
        }).ToList();
    }

    public async Task<ProductViewModel?> Get(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/products/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var product = JsonSerializer.Deserialize<ProductViewModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return product;
    }
    
    public async Task<bool> Delete(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("YourEcommerceApi");
        var response = await httpClient.DeleteAsync($"api/products/{id}");

        return response.IsSuccessStatusCode;
    }
}