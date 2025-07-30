using YourEcommerce.ViewModels;

namespace YourEcommerce.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductViewModel>> GetAllProducts();
}