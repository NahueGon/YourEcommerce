using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class TagService : ITagService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;

    public TagService(AppDbContext context, IMapper  mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagResponseDto>> GetAll()
    {
        var tags = await _context.Tags
            .Include(t => t.ProductTags)
            .ToListAsync();

        return _mapper.Map<List<TagResponseDto>>(tags);
    }

    public async Task<TagResponseDto?> Get(int id)
    {
        var tag = await _context.Tags
            .Include(t => t.ProductTags)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (tag == null) return null;

        return _mapper.Map<TagResponseDto>(tag);
    }

    public async Task<TagResponseDto> Save(TagCreateDto tagDto)
    {
        var tag = _mapper.Map<Tag>(tagDto);

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return _mapper.Map<TagResponseDto>(tag);
    }

    public async Task<TagResponseDto?> Update(int id, TagUpdateDto tagDto)
    {
        var currentTag = await _context.Tags.FindAsync(id);
        if (currentTag == null) return null;

        if (!string.IsNullOrWhiteSpace(tagDto.Name) && tagDto.Name != currentTag.Name)
            currentTag.Name = tagDto.Name;

        if (!string.IsNullOrWhiteSpace(tagDto.Group) && tagDto.Group != currentTag.Group)
            currentTag.Group = tagDto.Group;

        await _context.SaveChangesAsync();

        return _mapper.Map<TagResponseDto>(currentTag);
    }

    public async Task<bool> Delete(int id)
    {
        var currentTag = await _context.Tags.FindAsync(id);
        if (currentTag == null) return false;

        _context.Remove(currentTag);
        await _context.SaveChangesAsync();

        return true;
    }
}
