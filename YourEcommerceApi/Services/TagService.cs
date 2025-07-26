using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class TagService : ITagService
{
    private readonly AppDbContext _context;

    public TagService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TagResponseDto>> GetAll()
    {
        var tags = await _context.Tags
            .ToListAsync();

        return tags.Select(t => t.ToDto()).ToList();
    }

    public async Task<TagResponseDto?> Get(int id)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == id);
        if (tag == null) return null;

        return tag.ToDto();
    }

    public async Task<TagResponseDto> Save(TagCreateDto tagDto)
    {
        var tag = new Tag
        {
            Name = tagDto.Name,
            Group = tagDto.Group
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return tag.ToDto();
    }

    public async Task<bool> Update(int id, TagUpdateDto tagDto)
    {
        var currentTag = await _context.Tags.FindAsync(id);
        if (currentTag == null) return false;

        currentTag.Name = tagDto.Name;
        currentTag.Group = tagDto.Group;

        await _context.SaveChangesAsync();

        return true;
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
