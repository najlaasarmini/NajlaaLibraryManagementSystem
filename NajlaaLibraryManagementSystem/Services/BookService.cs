using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.Book;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class BookService : IBookService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<BookDto>> GetAllAsync()
        {
            var Books = await _mapper.ProjectTo<BookDto>(_context.Books).ToListAsync();
            return Books.AsQueryable();
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var Book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.BookID == id);

            if (Book == null)
            {
                throw new InvalidOperationException($"Book with ID {id} not found.");
            }

            return _mapper.Map<BookDto>(Book);
        }

        // GetBooksByAuthor - PROCEDURE
        public async Task<IEnumerable<BookDto>> GetBooksByTitleKeywordAsync(string keyword)
        {
            var books = await _context.Books
                .FromSqlRaw("EXECUTE dbo.GetBooksByTitleKeyword @Keyword", new SqlParameter("@Keyword", keyword))
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
        public async Task<int?> CreateAsync(CreateBookDto dto)
        {
            var BookEntity = _mapper.Map<Book>(dto);
            var validationErrors = ValidateObject(BookEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create Book: {string.Join(", ", validationErrors)}");
            }

            _context.Books.Add(BookEntity);
            await _context.SaveChangesAsync();

            return BookEntity.BookID;
        }

        public async Task UpdateAsync(UpdateBookDto dto, int id)
        {
            var BookEntity = await _context.Books.FirstOrDefaultAsync(x => x.BookID == id);

            if (BookEntity == null)
            {
                throw new InvalidOperationException($"Book with ID {id} not found.");
            }

            _mapper.Map(dto, BookEntity);

            var validationErrors = ValidateObject(BookEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update Book: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var BookEntity = await _context.Books.FirstOrDefaultAsync(x => x.BookID == id);

            if (BookEntity == null)
            {
                throw new InvalidOperationException($"Book with ID {id} not found.");
            }
            _context.Books.Remove(BookEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(Book BookEntity)
        {
            var validationErrors = new List<string>();

            if (_context.Books.Any(x => x.Title == BookEntity.Title && x.BookID != BookEntity.BookID))
            {
                validationErrors.Add("Book Name Exists");
            }

            return validationErrors;
        }
    }
}
