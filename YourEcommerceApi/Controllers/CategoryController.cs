using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs;
using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
        {
            var category = await _categoryService.Get(id);

            if (category == null)
                return NotFound("Categoría no encontrada");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategory(CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseDto = await _categoryService.Save(categoryDto);

            return CreatedAtAction(nameof(GetCategory), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, CategoryUpdateDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentCategory = await _categoryService.Get(id);

            if (currentCategory == null)
                return NotFound("Categoría no encontrada");

            await _categoryService.Update(id, categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var currentCategory = await _categoryService.Get(id);

            if (currentCategory == null)
                return NotFound("Categoría no encontrada");

            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}
