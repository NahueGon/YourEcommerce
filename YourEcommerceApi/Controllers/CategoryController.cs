using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.CategoryGender;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Tags("Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryGendersService _categoryGenderService;

        public CategoryController(ICategoryService categoryService, ICategoryGendersService categoryGenderService)
        {
            _categoryService = categoryService;
            _categoryGenderService = categoryGenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAll();
            if (categories == null || !categories.Any()) return NotFound("No se encontraron Categorias.");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null) return NotFound("Marca no encontrada");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.Save(categoryDto);
            return Ok(category);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, [FromForm] CategoryUpdateDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedCategory = await _categoryService.Update(id, categoryDto);
            if (updatedCategory == null) return NotFound("Categoria no encontrada");

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.Get(id);
            if (category == null) return NotFound("Categoria no encontrada");

            await _categoryService.Delete(id);

            return NoContent();
        }
    }
}