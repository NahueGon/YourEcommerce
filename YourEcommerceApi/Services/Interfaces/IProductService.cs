using YourEcommerceApi.Models;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
}
