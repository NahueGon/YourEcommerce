using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.GenderDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class GenderService : IGenderService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;
    private readonly IWebHostEnvironment _env;

    public GenderService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment env)
    {
        _context = dbContext;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IEnumerable<GenderResponseDto>> GetAll()
    {
        var genders = await _context.Genders
            .Include(b => b.Products)
            .ToListAsync();

        return _mapper.Map<List<GenderResponseDto>>(genders);
    }

    public async Task<GenderResponseDto?> Get(int id)
    {
        var gender = await _context.Genders
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);
        if (gender == null) return null;

        return _mapper.Map<GenderResponseDto>(gender);
    }

    public async Task<GenderResponseDto> Save(GenderCreateDto genderDto)
    {
        var gender = _mapper.Map<Gender>(genderDto);

        _context.Genders.Add(gender);
        await _context.SaveChangesAsync();

        return _mapper.Map<GenderResponseDto>(gender);
    }

    public async Task<GenderResponseDto?> Update(int id, GenderUpdateDto genderDto)
    {
        var currentGender = await _context.Genders.FindAsync(id);
        if (currentGender == null) return null;

        if (!string.IsNullOrWhiteSpace(genderDto.Name) && genderDto.Name != currentGender.Name)
            currentGender.Name = genderDto.Name;
            
        if (!string.IsNullOrWhiteSpace(genderDto.Description) && genderDto.Description != currentGender.Description)
            currentGender.Description = genderDto.Description;

        if (genderDto.GenderImage != null && genderDto.GenderImage.Length > 0)
        {
            if (!string.IsNullOrEmpty(currentGender.GenderImage))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath!, currentGender.GenderImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            currentGender.GenderImage = await FileUploadHelper.SaveFileAsync(
                _env,
                genderDto.GenderImage,
                $"img/genders/{currentGender.Id}_gender",
                $"{currentGender.Id}_frontpage",
                width: 630,
                height: 830
            );
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<GenderResponseDto>(currentGender);
    }

    public async Task<bool> Delete(int id)
    {
        var currentGender = await _context.Genders.FindAsync(id);
        if (currentGender == null) return false;
            
        _context.Remove(currentGender);
        await _context.SaveChangesAsync();

        return true;
    }
}