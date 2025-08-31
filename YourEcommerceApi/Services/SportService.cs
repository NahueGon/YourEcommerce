using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class SportService : ISportService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;
    private readonly IWebHostEnvironment _env;

    public SportService(AppDbContext dbConext, IMapper mapper, IWebHostEnvironment env)
    {
        _context = dbConext;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IEnumerable<SportResponseDto>> GetAll()
    {
        var sports = await _context.Sports
            .Include(b => b.Products)
            .ToListAsync();

        return _mapper.Map<List<SportResponseDto>>(sports);
    }
    
    public async Task<SportResponseDto?> Get(int id)
    {
        var sport = await _context.Sports
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sport == null) return null;

        return _mapper.Map<SportResponseDto>(sport);
    }

    public async Task<SportResponseDto> Save(SportCreateDto sportDto)
    {
        var sport = _mapper.Map<Sport>(sportDto);

        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();

        if (sportDto.SportImage != null && sportDto.SportImage.Length > 0)
        {
            sport.SportImage = await FileUploadHelper.SaveFileAsync(
                _env,
                sportDto.SportImage,
                $"img/sports/{sport.Id}_sport",
                $"{sport.Id}_frontpage",
                width: 1920,
                height: 1080
            );

            await _context.SaveChangesAsync();
        }

        return _mapper.Map<SportResponseDto>(sport);
    }

    public async Task<SportResponseDto?> Update(int id, SportUpdateDto sportDto)
    {
        var currentSport = await _context.Sports.FindAsync(id);
        if (currentSport == null) return null;

        if (!string.IsNullOrWhiteSpace(sportDto.Name) && sportDto.Name != currentSport.Name)
            currentSport.Name = sportDto.Name;

        if (!string.IsNullOrWhiteSpace(sportDto.Description) && sportDto.Description != currentSport.Name)
            currentSport.Description = sportDto.Description;

        if (sportDto.SportImage != null && sportDto.SportImage.Length > 0)
        {
            if (!string.IsNullOrEmpty(currentSport.SportImage))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath!, currentSport.SportImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            currentSport.SportImage = await FileUploadHelper.SaveFileAsync(
                _env,
                sportDto.SportImage,
                $"img/sports/{currentSport.Id}_sport",
                $"{currentSport.Id}_frontpage",
                width: 1920,
                height: 1080
            );
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<SportResponseDto>(currentSport);
    }

    public async Task<bool> Delete(int id)
    {
        var currentSport = await _context.Sports.FindAsync(id);
        if (currentSport == null) return false;
            
        _context.Remove(currentSport);
        await _context.SaveChangesAsync();

        return true;
    }
}