using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class TagExtension
{
    public static TagResponseDto ToDto(this Tag tag)
    {
        return new TagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
}