using YourEcommerceApi.DTOs.ProductAttributeDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductAttributeService
{
    Task<IEnumerable<ProductAttributeResponseDto>> GetAll();
    Task<ProductAttributeResponseDto?> Get(int id);
    Task<ProductAttributeResponseDto> Save(ProductAttributeCreateDto tagDto);
    Task<bool> Update(int id, ProductAttributeUpdateDto tagDto);
    Task<bool> Delete(int id);
}
