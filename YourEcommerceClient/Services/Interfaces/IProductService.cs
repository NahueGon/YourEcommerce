using YourEcommerceClient.Models;

namespace YourEcommerceClient.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync();
}
