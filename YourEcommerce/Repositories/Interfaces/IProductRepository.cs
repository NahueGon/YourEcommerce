using YourEcommerce.DTOs.ProductDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> GetById(int id);
    Task<ProductResponseDto?> Create(ProductCreateDto productDto);
    Task<ProductResponseDto?> Update(int id, ProductUpdateDto productDto);
    Task<bool> Delete(int id);
}