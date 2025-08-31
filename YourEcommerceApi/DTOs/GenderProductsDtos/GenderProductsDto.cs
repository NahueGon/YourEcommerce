using YourEcommerceApi.DTOs.ProductDtos;

namespace YourEcommerceApi.DTOs.GenderProductsDtos;

public class GenderProductsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}