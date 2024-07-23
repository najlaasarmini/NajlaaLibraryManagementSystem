using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.Publisher;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/Publishers")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _PublisherService;

        public PublisherController(IPublisherService PublisherService)
        {
            _PublisherService = PublisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            try
            {
                var Publishers = await _PublisherService.GetAllAsync();
                return Ok(Publishers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Publishers: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            try
            {
                var Publisher = await _PublisherService.GetByIdAsync(id);
                return Ok(Publisher);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Publisher: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto PublisherDto)
        {
            try
            {
                var PublisherId = await _PublisherService.CreateAsync(PublisherDto);
                return CreatedAtAction(nameof(GetPublisherById), new { id = PublisherId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create Publisher: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] UpdatePublisherDto PublisherDto)
        {
            try
            {
                await _PublisherService.UpdateAsync(PublisherDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update Publisher: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                await _PublisherService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete Publisher: {ex.Message}");
            }
        }
    }
}
