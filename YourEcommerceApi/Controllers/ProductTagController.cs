using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/product-tags")]
    [ApiController]
    [Tags("ProductTags")]
    public class ProductTagController : ControllerBase
    {
        private readonly IProductTagService _productTagService;

        public ProductTagController(IProductTagService service)
        {
            _productTagService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTagResponseDto>>> GetProductTags()
        {
            var productTags = await _productTagService.GetAll();
            if (productTags == null || !productTags.Any()) return NotFound("No se encontraron Relaciones Producto-Etiqueta.");

            return Ok(productTags);
        }

        [HttpGet("{productId}/{tagId}")]
        public async Task<ActionResult<ProductTagResponseDto>> GetProductTag(int productId, int tagId)
        {
            var productTag = await _productTagService.Get(productId, tagId);
            if (productTag == null) return NotFound("Relación Producto-Etiqueta no encontrada.");

            return Ok(productTag);
        }

        [HttpPost]
        public async Task<ActionResult<ProductTagResponseDto>> CreateProductTag(ProductTagCreateDto productTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var responseDto = await _productTagService.Save(productTagDto);

            return CreatedAtAction(nameof(GetProductTag),
                new { productId = responseDto.Product.Id, tagId = responseDto.Tag.Id },
                responseDto);
        }

        [HttpPut("{productId}/{tagId}")]
        public async Task<ActionResult> UpdateProductTag(int productId, int tagId, ProductTagUpdateDto productTagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProductTag = await _productTagService.Get(productId, tagId);
            if (existingProductTag == null) return NotFound("Relación Producto-Etiqueta no encontrada.");

            var updated = await _productTagService.Update(productId, tagId, productTagDto);
            if (!updated) return BadRequest("No se pudo actualizar la relación.");

            return NoContent();
        }

        [HttpDelete("{productId}/{tagId}")]
        public async Task<ActionResult> DeleteProductTag(int productId, int tagId)
        {
            var productTag = await _productTagService.Get(productId, tagId);
            if (productTag == null) return NotFound("Relación Producto-Etiqueta no encontrada");

            await _productTagService.Delete(productId, tagId);

            return NoContent();
        }
    }
}