using NajlaaLibraryManagementSystem.Dtos.SubCategory;

namespace NajlaaLibraryManagementSystem.Services.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IQueryable<SubCategoryDto>> GetAllAsync();
        Task<SubCategoryDto> GetByIdAsync(int id);
        Task<List<SubCategoryDto>> GetByParentCategoryAsync(int parentCategoryId);
        Task<int?> CreateAsync(CreateSubCategoryDto dto);
        Task UpdateAsync(UpdateSubCategoryDto dto, int id);
        Task DeleteAsync(int id);
    }
}
