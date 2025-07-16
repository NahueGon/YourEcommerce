using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> Get(int id);
    Task<IEnumerable<ProductResponseDto>> GetAllByType(string type);
    Task<ProductResponseDto> SaveCloth(ClothCreateDto clothDto);
    Task<ProductResponseDto> SaveShoe(ShoeCreateDto shoeDto);
    Task<ProductResponseDto> SaveAccessory(AccessoryCreateDto accessoryDto);
    Task<bool> Delete(int id);
}
