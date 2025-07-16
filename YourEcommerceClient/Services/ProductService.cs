using YourEcommerceClient.Models;
using YourEcommerceClient.Services.Interfaces;

namespace YourEcommerceClient.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/Product") ?? new List<ProductDto>();
    }
}
