using YourEcommerceApi.DTOs.TagDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface ITagService
{
    Task<IEnumerable<TagResponseDto>> GetAll();
    Task<TagResponseDto?> Get(int id);
    Task<TagResponseDto> Save(TagCreateDto tagDto);
    Task<bool> Update(int id, TagUpdateDto tagDto);
    Task<bool> Delete(int id);
}
