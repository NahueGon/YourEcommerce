using YourEcommerce.DTOs.ProductDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllForTable();
    Task<ProductUpdateDto?> GetForEdit(int id);
    Task<ProductDto?> Create(ProductCreateDto productDto);
    Task<ProductDto?> Update(int id, ProductUpdateDto productDto);
    Task<bool> Delete(int id);
}