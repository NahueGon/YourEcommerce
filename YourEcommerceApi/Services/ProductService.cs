using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;

    public ProductService(AppDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAll()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Sport)
            .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductVariants)
                .ThenInclude(v => v.Colors)
            .ToListAsync();

        return _mapper.Map<List<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> Get(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Sport)
            .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductVariants)
                .ThenInclude(v => v.Colors)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return null;

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> Save(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        Brand? brand = null;
        Sport? sport = null;
        Category? category = null;

        if (productDto.CategoryId is > 0)
        {
            category = await _context.Categories.FindAsync(productDto.CategoryId)
                ?? throw new Exception("Categoria no encontrada.");
        }

        if (productDto.BrandId is > 0)
        {
            brand = await _context.Brands.FindAsync(productDto.BrandId)
                ?? throw new Exception("Marca no encontrada.");
        }

        if (productDto.SportId is > 0)
        {
            sport = await _context.Sports.FindAsync(productDto.SportId)
                ?? throw new Exception("Deporte no encontrado.");
        }

        product.ProductTags = new List<ProductTag>();
        foreach (var tagDto in productDto.ProductTags)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagDto.Name && t.Group == tagDto.Group);
            var tag = existingTag ?? new Tag { Name = tagDto.Name, Group = tagDto.Group };

            if (existingTag == null)
                _context.Tags.Add(tag);

            product.ProductTags.Add(new ProductTag { Tag = tag });
        }

        foreach(var variant in product.ProductVariants)
        {
            Console.WriteLine($"Variant Size: {variant.Size}, Stock: {variant.Stock}");
            if (variant.Stock <= 0)
            {
                throw new Exception($"Stock inválido para variante tamaño {variant.Size}. Valor: {variant.Stock}");
            }
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto?> Update(int id, ProductUpdateDto productDto)
    {
        var product = await _context.Products
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Colors)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        await ValidateRelatedEntities(productDto);
        UpdateBasicFields(product, productDto);

        if (productDto.ProductVariants != null)
        {
            var variantDtos = _mapper.Map<ICollection<ProductVariantDto>>(productDto.ProductVariants);
            await UpdateVariants(id, variantDtos);
        }

        if (productDto.ProductAttributes != null)
        {
            var attributeDtos = _mapper.Map<ICollection<ProductAttributeDto>>(productDto.ProductAttributes);
            await UpdateAttributes(id, attributeDtos); 
        }         
        
        if (productDto.ProductTags != null)
        {
            var mappedTags = _mapper.Map<ICollection<ProductTagDto>>(productDto.ProductTags);
            await UpdateTags(id, mappedTags);
        }

        await _context.SaveChangesAsync();

        var updated = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Sport)
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Colors)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductResponseDto>(updated);
    }

    private async Task ValidateRelatedEntities(ProductUpdateDto dto)
    {
        if (dto.CategoryId.HasValue && !await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId))
            throw new Exception("Categoría no encontrada.");

        if (dto.BrandId.HasValue && !await _context.Brands.AnyAsync(b => b.Id == dto.BrandId))
            throw new Exception("Marca no encontrada.");

        if (dto.SportId.HasValue && !await _context.Sports.AnyAsync(s => s.Id == dto.SportId))
            throw new Exception("Deporte no encontrado.");
    }

    private void UpdateBasicFields(Product product, ProductUpdateDto dto)
    {
        product.Name = dto.Name ?? product.Name;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price != 0 ? dto.Price : product.Price;
        product.Gender = dto.Gender ?? product.Gender;
        product.CategoryId = dto.CategoryId ?? product.CategoryId;
        product.BrandId = dto.BrandId ?? product.BrandId;
        product.SportId = dto.SportId ?? product.SportId;
    }

    private async Task UpdateVariants(int productId, ICollection<ProductVariantDto> variants)
    {
        var existingVariants = await _context.ProductVariants
            .Where(pv => pv.ProductId == productId)
            .ToListAsync();
        _context.ProductVariants.RemoveRange(existingVariants);

        foreach (var variantDto in variants)
        {
            var variant = _mapper.Map<ProductVariant>(variantDto);
            variant.ProductId = productId;
            _context.ProductVariants.Add(variant);
        }
    }

    private async Task UpdateAttributes(int productId, ICollection<ProductAttributeDto> attributes)
    {
        var existingAttributes = await _context.ProductAttributes
            .Where(pa => pa.ProductId == productId)
            .ToListAsync();
        _context.ProductAttributes.RemoveRange(existingAttributes);

        foreach (var attrDto in attributes)
        {
            var attr = _mapper.Map<ProductAttribute>(attrDto);
            attr.ProductId = productId;
            _context.ProductAttributes.Add(attr);
        }
    }

   private async Task UpdateTags(int productId, ICollection<ProductTagDto> tagDtos)
    {
        var product = await _context.Products
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == productId)
            ?? throw new Exception("Producto no encontrado");

        foreach (var tagDto in tagDtos)
        {
            if (tagDto.Tag == null)
                    throw new ArgumentException("Cada tag debe tener un objeto Tag no nulo.");

            if (string.IsNullOrWhiteSpace(tagDto.Tag.Name))
                throw new ArgumentException($"El Tag con Group '{tagDto.Tag.Group}' tiene un Name nulo o vacío.");

            var tag = await _context.Tags
                .FirstOrDefaultAsync(t => t.Name == tagDto.Tag.Name && t.Group == tagDto.Tag.Group);

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagDto.Tag.Name,
                    Group = tagDto.Tag.Group
                };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }

            if (!product.ProductTags.Any(pt => pt.TagId == tag.Id))
            {
                product.ProductTags.Add(new ProductTag
                {
                    ProductId = product.Id,
                    TagId = tag.Id,
                    Tag = tag
                });
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}