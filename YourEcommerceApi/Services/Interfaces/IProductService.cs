using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> Get(int id);
    Task<ProductResponseDto> Save(ProductCreateDto productDto);
    Task<ProductResponseDto?> Update(int id, ProductUpdateDto productDto);
    Task<bool> Delete(int id);
}
