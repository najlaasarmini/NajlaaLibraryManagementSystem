using NajlaaLibraryManagementSystem.Dtos.Book;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface IBookService
    {
        Task<IQueryable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByTitleKeywordAsync(string keyword);
        Task<int?> CreateAsync(CreateBookDto dto);
        Task UpdateAsync(UpdateBookDto dto, int id);
        Task DeleteAsync(int id);
    }
}
