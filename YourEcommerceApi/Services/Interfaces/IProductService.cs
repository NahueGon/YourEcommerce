using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> Get(int id);
    Task<ProductResponseDto> SaveAccessory(AccessoryCreateDto accesoryDto);
    Task<ProductResponseDto> SaveShoe(ShoeCreateDto shoeDto);
    Task<ProductResponseDto> SaveCloth(ClothCreateDto clothDto);
}
