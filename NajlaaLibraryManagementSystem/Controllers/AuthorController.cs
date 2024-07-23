using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.Author;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/Authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _AuthorService;

        public AuthorController(IAuthorService AuthorService)
        {
            _AuthorService = AuthorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var Authors = await _AuthorService.GetAllAsync();
                return Ok(Authors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Authors: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var Author = await _AuthorService.GetByIdAsync(id);
                return Ok(Author);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Author: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto AuthorDto)
        {
            try
            {
                var AuthorId = await _AuthorService.CreateAsync(AuthorDto);
                return CreatedAtAction(nameof(GetAuthorById), new { id = AuthorId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create Author: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto AuthorDto)
        {
            try
            {
                await _AuthorService.UpdateAsync(AuthorDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update Author: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _AuthorService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete Author: {ex.Message}");
            }
        }
    }
}
