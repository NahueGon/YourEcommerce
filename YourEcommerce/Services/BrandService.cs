using AutoMapper;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository repository, IMapper mapper)
    {
        _brandRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<BrandDto>> GetAllForTable()
    {
        var brands = await _brandRepository.GetAll();
        return brands.Select(s => _mapper.Map<BrandDto>(s)).ToList();
    }

    public async Task<BrandUpdateDto?> GetForEdit(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand is null) return null;
        return _mapper.Map<BrandUpdateDto>(brand);
    }

       public async Task<BrandDto?> Create(BrandCreateDto brandDto)
    {
        var created = await _brandRepository.Create(brandDto);
        if (created == null) return null;
        return _mapper.Map<BrandDto>(created);
    }

    public async Task<BrandDto?> Update(int id, BrandUpdateDto brandDto)
    {
        var updated = await _brandRepository.Update(id, brandDto);
        if (updated == null) return null;
        return _mapper.Map<BrandDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _brandRepository.Delete(id);
    }
}