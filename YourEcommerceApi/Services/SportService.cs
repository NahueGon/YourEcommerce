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

    public SportService(AppDbContext dbConext, IMapper mapper)
    {
        _context = dbConext;
        _mapper = mapper;
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