using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.Language;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/Languages")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _LanguageService;

        public LanguageController(ILanguageService LanguageService)
        {
            _LanguageService = LanguageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            try
            {
                var Languages = await _LanguageService.GetAllAsync();
                return Ok(Languages);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Languages: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLanguageById(int id)
        {
            try
            {
                var Language = await _LanguageService.GetByIdAsync(id);
                return Ok(Language);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Language: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] CreateLanguageDto LanguageDto)
        {
            try
            {
                var LanguageId = await _LanguageService.CreateAsync(LanguageDto);
                return CreatedAtAction(nameof(GetLanguageById), new { id = LanguageId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create Language: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] UpdateLanguageDto LanguageDto)
        {
            try
            {
                await _LanguageService.UpdateAsync(LanguageDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update Language: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            try
            {
                await _LanguageService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete Language: {ex.Message}");
            }
        }
    }
}
