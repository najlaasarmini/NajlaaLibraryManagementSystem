using NajlaaLibraryManagementSystem.Dtos.ParentCategory;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface IParentCategoryService
    {
        Task<IQueryable<ParentCategoryDto>> GetAllAsync();
        Task<ParentCategoryDto> GetByIdAsync(int id);
        Task<int?> CreateAsync(CreateParentCategoryDto dto);
        Task UpdateAsync(UpdateParentCategoryDto dto, int id);
        Task DeleteAsync(int id);
    }
}
