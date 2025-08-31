using AutoMapper;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _productRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllForTable()
    {
        var sports = await _productRepository.GetAll();
        return sports.Select(s => _mapper.Map<ProductDto>(s)).ToList();
    }

    public async Task<ProductUpdateDto?> GetForEdit(int id)
    {
        var product = await _productRepository.GetById(id);
        if (product is null) return null;
        return _mapper.Map<ProductUpdateDto>(product);
    }

    public async Task<ProductDto?> Create(ProductCreateDto productDto)
    {
        var created = await _productRepository.Create(productDto);
        if (created == null) return null;
        return _mapper.Map<ProductDto>(created);
    }
    
    public async Task<ProductDto?> Update(int id, ProductUpdateDto productDto)
    {
        var updated = await _productRepository.Update(id, productDto);
        if (updated == null) return null;
        return _mapper.Map<ProductDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _productRepository.Delete(id);
    }
}