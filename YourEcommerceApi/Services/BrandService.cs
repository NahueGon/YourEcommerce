using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class BrandService : IBrandService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;

    public BrandService(AppDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BrandResponseDto>> GetAll()
    {
        var brands = await _context.Brands
            .Include(b => b.Products)
            .ToListAsync();

        return _mapper.Map<List<BrandResponseDto>>(brands);
    }

    public async Task<BrandResponseDto?> Get(int id)
    {
        var brand = await _context.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (brand == null) return null;

        return _mapper.Map<BrandResponseDto>(brand);
    }

    public async Task<BrandResponseDto> Save(BrandCreateDto brandDto)
    {
        var brand = _mapper.Map<Brand>(brandDto);

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return _mapper.Map<BrandResponseDto>(brand);
    }

    public async Task<BrandResponseDto?> Update(int id, BrandUpdateDto brandDto)
    {
        var currentBrand = await _context.Brands.FindAsync(id);
        if (currentBrand == null) return null;

        if (!string.IsNullOrWhiteSpace(brandDto.Name) && brandDto.Name != currentBrand.Name)
            currentBrand.Name = brandDto.Name;
            
        if (!string.IsNullOrWhiteSpace(brandDto.Description) && brandDto.Description != currentBrand.Description)
            currentBrand.Description = brandDto.Description;

        await _context.SaveChangesAsync();

        return _mapper.Map<BrandResponseDto>(currentBrand);
    }

    public async Task<bool> Delete(int id)
    {
        var currentBrand = await _context.Brands.FindAsync(id);
        if (currentBrand == null) return false;
            
        _context.Remove(currentBrand);
        await _context.SaveChangesAsync();

        return true;
    }
}