using AutoMapper;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class SportService : ISportService
{
    private readonly ISportRepository _sportRepository;
    private readonly IMapper _mapper;

    public SportService(ISportRepository repository, IMapper mapper)
    {
        _sportRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<SportDto>> GetAllForTable()
    {
        var sports = await _sportRepository.GetAll();
        return sports.Select(s => _mapper.Map<SportDto>(s)).ToList();
    }

    public async Task<SportUpdateDto?> GetForEdit(int id)
    {
        var sport = await _sportRepository.GetById(id);
        if (sport is null) return null;
        return _mapper.Map<SportUpdateDto>(sport);
    }

    public async Task<SportDto?> Create(SportCreateDto sportDto)
    {
        var created = await _sportRepository.Create(sportDto);
        if (created == null) return null;
        return _mapper.Map<SportDto>(created);
    }

    public async Task<SportDto?> Update(int id, SportUpdateDto sportDto)
    {
        var updated = await _sportRepository.Update(id, sportDto);
        if (updated == null) return null;
        return _mapper.Map<SportDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _sportRepository.Delete(id);
    }
}