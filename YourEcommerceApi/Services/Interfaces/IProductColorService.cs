using YourEcommerceApi.DTOs.ProductColorDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductColorService
{
    Task<IEnumerable<ProductColorResponseDto>> GetAll();
    Task<ProductColorResponseDto?> Get(int id);
    Task<ProductColorResponseDto> Save(ProductColorCreateDto tagDto);
    Task<bool> Update(int id, ProductColorUpdateDto tagDto);
    Task<bool> Delete(int id);
}