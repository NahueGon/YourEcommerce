using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class SportExtension
{
    public static SportResponseDto ToDto(this Sport sport)
    {
        return new SportResponseDto
        {
            Id = sport.Id,
            Name = sport.Name
        };
    }
}