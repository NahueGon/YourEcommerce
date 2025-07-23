using YourEcommerceApi.DTOs.ProductType;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductTypeService
{
    Task<IEnumerable<ProductTypeResponseDto>> GetAll();
    Task<ProductTypeResponseDto?> Get(int id);
    Task<ProductTypeResponseDto> Save(ProductTypeCreateDto productTypeDto);
    Task<ProductTypeResponseDto?> Update(int id, ProductTypeUpdateDto productTypeDto);
    Task<bool> Delete(int id);
}
