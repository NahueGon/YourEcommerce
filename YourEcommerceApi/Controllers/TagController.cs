using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/tags")]
    [ApiController]
    [Tags("Tags")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService service)
        {
            _tagService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagResponseDto>>> GetTags()
        {
            var tags = await _tagService.GetAll();
            if (tags == null || !tags.Any()) return NotFound("No se encontraron etiquetas.");

            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagResponseDto>> GetTag(int id)
        {
            var tag = await _tagService.Get(id);
            if (tag == null) return NotFound("Etiqueta no encontrada.");

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<TagResponseDto>> CreateTag(TagCreateDto tagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var responseDto = await _tagService.Save(tagDto);

            return CreatedAtAction(nameof(GetTag), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TagResponseDto>> UpdateTag(int id, TagUpdateDto tagDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _tagService.Get(id);
            if (updated == null) return NotFound("Etiqueta no encontrada.");

            var updatedTag = await _tagService.Update(id, tagDto);
            if (updatedTag == null) return NotFound("Etiqueta no encontrada.");

            return Ok(updatedTag);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTag(int id)
        {
            var sport = await _tagService.Get(id);
            if (sport == null) return NotFound("Etiqueta no encontrada");

            await _tagService.Delete(id);

            return NoContent();
        }
    }
}