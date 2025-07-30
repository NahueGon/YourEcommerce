using AutoMapper;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.TagDtos;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<TagCreateDto, Tag>();
        CreateMap<Tag, TagResponseDto>();
        
        CreateMap<ProductTag, ProductTagDto>();
        CreateMap<ProductTagCreateDto, ProductTag>();
        CreateMap<ProductTagCreateDto, ProductTagDto>();

        CreateMap<ProductTag, ProductTagCreateDto>();
    }
}