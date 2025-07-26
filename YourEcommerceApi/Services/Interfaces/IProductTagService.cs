using YourEcommerceApi.DTOs.ProductTagDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductTagService
{
    Task<IEnumerable<ProductTagResponseDto>> GetAll();
    Task<ProductTagResponseDto?> Get(int productId, int tagId);
    Task<ProductTagResponseDto> Save(ProductTagCreateDto productTagDto);
    Task<bool> Update(int productId, int tagId, ProductTagUpdateDto productTagDto);
    Task<bool> Delete(int productId, int tagId);
}
