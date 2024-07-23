using NajlaaLibraryManagementSystem.Dtos.Language;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<IQueryable<LanguageDto>> GetAllAsync();
        Task<LanguageDto> GetByIdAsync(int id);
        Task<int?> CreateAsync(CreateLanguageDto dto);
        Task UpdateAsync(UpdateLanguageDto dto, int id);
        Task DeleteAsync(int id);
    }
}
