using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.SubCategory;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/SubCategories")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _SubCategoryService;

        public SubCategoryController(ISubCategoryService SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            try
            {
                var SubCategories = await _SubCategoryService.GetAllAsync();
                return Ok(SubCategories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get SubCategories: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            try
            {
                var SubCategory = await _SubCategoryService.GetByIdAsync(id);
                return Ok(SubCategory);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get SubCategory: {ex.Message}");
            }
        }

        [HttpGet("ByParentCategory/{parentCategoryId}")]
        public async Task<IActionResult> GetSubCategoriesByParentCategory(int parentCategoryId)
        {
            try
            {
                var subCategories = await _SubCategoryService.GetByParentCategoryAsync(parentCategoryId);
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get SubCategories for ParentCategoryId {parentCategoryId}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryDto SubCategoryDto)
        {
            try
            {
                var SubCategoryId = await _SubCategoryService.CreateAsync(SubCategoryDto);
                return CreatedAtAction(nameof(GetSubCategoryById), new { id = SubCategoryId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create SubCategory: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] UpdateSubCategoryDto SubCategoryDto)
        {
            try
            {
                await _SubCategoryService.UpdateAsync(SubCategoryDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update SubCategory: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                await _SubCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete SubCategory: {ex.Message}");
            }
        }
    }
}
