using YourEcommerceApi.DTOs.ProductVariantDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductVariantService
{
    Task<IEnumerable<ProductVariantResponseDto>> GetAll();
    Task<ProductVariantResponseDto?> Get(int id);
    Task<ProductVariantResponseDto> Save(ProductVariantCreateDto productVariantDto);
    Task<bool> Update(int id, ProductVariantUpdateDto productVariantDto);
    Task<bool> Delete(int id);
}