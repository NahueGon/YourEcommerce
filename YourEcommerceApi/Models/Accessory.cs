using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.Models;

public class Accessory : Product
{
    public required string Type { get; set; }

    public override ProductResponseDto ToDto()
    {
        return new ProductResponseDto
        {
            Name = this.Name,
            Description = this.Description,
        };
    }
}
