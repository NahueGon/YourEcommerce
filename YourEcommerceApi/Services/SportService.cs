using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.Sport;
using YourEcommerceApi.Models;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class SportService : ISportService
{
    AppDbContext _context;

    public SportService(AppDbContext dbConext)
    {
        _context = dbConext;
    }

    public async Task<IEnumerable<SportResponseDto>> GetAll()
    {
        var sports = await _context.Sports
            .Include(s => s.Shoes)
            .Include(s => s.Clothes)
            .ToListAsync();

        return sports.Select(s => new SportResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Products = s.Shoes?
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList() ?? new List<ProductResponseDto>()
        });
    }
    
    public async Task<SportResponseDto?> Get(int id)
    {
        var sport = await _context.Sports
            .Include(s => s.Shoes)
            .Include(s => s.Clothes)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sport == null)
            return null;

        return new SportResponseDto
        {
            Id = sport.Id,
            Name = sport.Name,
            Description = sport.Description,
            Products = sport.Shoes?
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList() ?? new List<ProductResponseDto>()
        };
    }

    public async Task<SportResponseDto> Save(SportCreateDto sportDto)
    {
        var sport = new Sport
        {
            Name = sportDto.Name,
            Description = sportDto.Description,
        };

        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();

        return new SportResponseDto
        {
            Id = sport.Id,
            Name = sport.Name,
            Description = sport.Description,
        };
    }

    public async Task<bool> Update(int id, SportUpdateDto sportDto)
    {
        var currentSport = await _context.Sports.FindAsync(id);

        if (currentSport == null)
            return false;

        currentSport.Name = sportDto.Name;
        currentSport.Description = sportDto.Description;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var currentSport = await _context.Sports.FindAsync(id);

        if (currentSport == null)
            return false;
            
        _context.Remove(currentSport);
        await _context.SaveChangesAsync();

        return true;
    }
}
