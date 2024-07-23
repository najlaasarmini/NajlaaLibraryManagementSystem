using NajlaaLibraryManagementSystem.Dtos.Publisher;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<IQueryable<PublisherDto>> GetAllAsync();
        Task<PublisherDto> GetByIdAsync(int id);
        Task<int?> CreateAsync(CreatePublisherDto dto);
        Task UpdateAsync(UpdatePublisherDto dto, int id);
        Task DeleteAsync(int id);
    }
}
