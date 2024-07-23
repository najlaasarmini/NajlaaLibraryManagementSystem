using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.Country;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/Countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _CountryService;

        public CountryController(ICountryService CountryService)
        {
            _CountryService = CountryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                var Countries = await _CountryService.GetAllAsync();
                return Ok(Countries);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Countries: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var Country = await _CountryService.GetByIdAsync(id);
                return Ok(Country);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Country: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto CountryDto)
        {
            try
            {
                var CountryId = await _CountryService.CreateAsync(CountryDto);
                return CreatedAtAction(nameof(GetCountryById), new { id = CountryId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create Country: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDto CountryDto)
        {
            try
            {
                await _CountryService.UpdateAsync(CountryDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update Country: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                await _CountryService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete Country: {ex.Message}");
            }
        }
    }
}
