using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductViewModel>> GetAll();
    Task<IEnumerable<ProductDto>> GetAllFlat();
    Task<ProductViewModel?> Get(int id);
    Task<bool> Delete(int id);
}