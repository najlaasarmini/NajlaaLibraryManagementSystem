using NajlaaLibraryManagementSystem.Dtos.Author;


namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IQueryable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task<int?> CreateAsync(CreateAuthorDto dto);
        Task UpdateAsync(UpdateAuthorDto dto, int id);
        Task DeleteAsync(int id);
    }
}
