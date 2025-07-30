using System.Text.Json;
using System.Text.Json.Serialization;
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

    public async Task<List<ProductViewModel>> GetAllProducts()
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
}