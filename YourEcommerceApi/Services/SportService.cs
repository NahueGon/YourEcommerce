using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class SportService : ISportService
{
    private readonly AppDbContext _context;

    public SportService(AppDbContext dbConext)
    {
        _context = dbConext;
    }

    public async Task<IEnumerable<SportResponseDto>> GetAll()
    {
        var sports = await _context.Sports
            .Include(b => b.Products)
            .ToListAsync();

        return sports.Select(b => b.ToDto()).ToList();
    }
    
    public async Task<SportResponseDto?> Get(int id)
    {
        var sport = await _context.Sports
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sport == null) return null;

        return sport.ToDto();
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

        return sport.ToDto();
    }

    public async Task<bool> Update(int id, SportUpdateDto sportDto)
    {
        var currentSport = await _context.Sports.FindAsync(id);
        if (currentSport == null) return false;

        currentSport.Name = sportDto.Name;
        currentSport.Description = sportDto.Description;

        await _context.SaveChangesAsync();

        return true;
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
