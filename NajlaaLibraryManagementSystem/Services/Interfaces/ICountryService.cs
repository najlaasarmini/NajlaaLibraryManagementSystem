using NajlaaLibraryManagementSystem.Dtos.Country;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IQueryable<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task<int?> CreateAsync(CreateCountryDto dto);
        Task UpdateAsync(UpdateCountryDto dto, int id);
        Task DeleteAsync(int id);
    }
}
