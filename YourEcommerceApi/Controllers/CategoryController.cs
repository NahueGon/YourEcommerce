using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Tags("Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAll();
            if (categories == null || !categories.Any()) return NotFound("No se encontraron categorias.");
            
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null) return NotFound("Categoría no encontrada");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategory(CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var responseDto = await _categoryService.Save(categoryDto);

            return CreatedAtAction(nameof(GetCategory), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, CategoryUpdateDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _categoryService.Get(id);
            if (updated == null) return NotFound("Categoría no encontrada");
            
            var updatedCategory = await _categoryService.Update(id, categoryDto);
            if (updatedCategory == null) return NotFound("Categoría no encontrada");

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.Get(id);
            if (deleted == null) return NotFound("Categoría no encontrada");

            await _categoryService.Delete(id);

            return NoContent();
        }
    }
}