using Microsoft.AspNetCore.Mvc;
using NajlaaLibraryManagementSystem.Dtos.Book;
using NajlaaLibraryManagementSystem.Services;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/Books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;

        public BookController(IBookService BookService)
        {
            _BookService = BookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var Books = await _BookService.GetAllAsync();
                return Ok(Books);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Books: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var Book = await _BookService.GetByIdAsync(id);
                return Ok(Book);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Book: {ex.Message}");
            }
        }

        // GetBooksByAuthor - PROCEDURE

        [HttpGet("search")]
        public async Task<IActionResult> GetBooksByTitleKeyword([FromQuery] string keyword)
        {
            try
            {
                var books = await _BookService.GetBooksByTitleKeywordAsync(keyword);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get Books: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto BookDto)
        {
            try
            {
                var BookId = await _BookService.CreateAsync(BookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = BookId }, null);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create Book: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto BookDto)
        {
            try
            {
                await _BookService.UpdateAsync(BookDto, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Failed to update Book: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _BookService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete Book: {ex.Message}");
            }
        }
    }
}
