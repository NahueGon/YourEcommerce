using AutoMapper;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class GenderService : IGenderService
{
    private readonly IGenderRepository _genderRepository;
    private readonly IMapper _mapper;

    public GenderService(IGenderRepository repository, IMapper mapper)
    {
        _genderRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<GenderDto>> GetAllForTable()
    {
        var genders = await _genderRepository.GetAll();
        return genders.Select(p => _mapper.Map<GenderDto>(p)).ToList();
    }

    public async Task<GenderUpdateDto?> GetForEdit(int id)
    {
        var gender = await _genderRepository.GetById(id);
        if (gender is null) return null;
        return _mapper.Map<GenderUpdateDto>(gender);
    }

    public async Task<GenderDto?> Create(GenderCreateDto genderDto)
    {
        var created = await _genderRepository.Create(genderDto);
        if (created == null) return null;
        return _mapper.Map<GenderDto>(created);
    }

    public async Task<GenderDto?> Update(int id, GenderUpdateDto genderDto)
    {
        var updated = await _genderRepository.Update(id, genderDto);
        if (updated == null) return null;
        return _mapper.Map<GenderDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _genderRepository.Delete(id);
    }
}