using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.ParentCategory;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/ParentCategories")]
    public class ParentCategoryController : ControllerBase
    {
        private readonly IParentCategoryService _ParentCategoryService;

        public ParentCategoryController(IParentCategoryService ParentCategoryService)
        {
            _ParentCategoryService = ParentCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParentCategories()
        {
            try
            {
                var ParentCategories = await _ParentCategoryService.GetAllAsync();
                return Ok(ParentCategories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get ParentCategories: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParentCategoryById(int id)
        {
            try
            {
                var ParentCategory = await _ParentCategoryService.GetByIdAsync(id);
                return Ok(ParentCategory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get ParentCategory: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateParentCategory([FromBody] CreateParentCategoryDto ParentCategoryDto)
        {
            try
            {
                var ParentCategoryId = await _ParentCategoryService.CreateAsync(ParentCategoryDto);
                return CreatedAtAction(nameof(GetParentCategoryById), new { id = ParentCategoryId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create ParentCategory: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParentCategory(int id, [FromBody] UpdateParentCategoryDto ParentCategoryDto)
        {
            try
            {
                await _ParentCategoryService.UpdateAsync(ParentCategoryDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update ParentCategory: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParentCategory(int id)
        {
            try
            {
                await _ParentCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete ParentCategory: {ex.Message}");
            }
        }
    }
}
