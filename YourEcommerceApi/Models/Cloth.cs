using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.Models;

public class Cloth : Product
{
    public string? Size { get; set; }
    public int SportId { get; set; }
    public Sport? Sport { get; set; }

    public override ProductResponseDto ToDto()
    {
        return new ProductResponseDto
        {
            Name = Name,
            Description = this.Description,
            Size = Size,
            Sport = Sport?.Name
        };
    }
}
