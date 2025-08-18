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
    private readonly IWebHostEnvironment _env;

    public BrandService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment env)
    {
        _context = dbContext;
        _mapper = mapper;
        _env = env;
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

        if (brandDto.BrandImage != null && brandDto.BrandImage.Length > 0)
        {
            if (!string.IsNullOrEmpty(currentBrand.BrandImage))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath!, currentBrand.BrandImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            currentBrand.BrandImage = await FileUploadHelper.SaveFileAsync(
                _env,
                brandDto.BrandImage,
                $"img/brands/{currentBrand.Id}_brand",
                $"{currentBrand.Id}_frontpage",
                width: 1000,
                height: 1000
            );
        }

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