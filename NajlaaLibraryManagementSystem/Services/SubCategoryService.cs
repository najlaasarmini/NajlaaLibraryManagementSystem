using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.SubCategory;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class SubCategoryService : ISubCategoryService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<SubCategoryDto>> GetAllAsync()
        {
            var SubCategories = await _mapper.ProjectTo<SubCategoryDto>(_context.SubCategories).ToListAsync();
            return SubCategories.AsQueryable();
        }

        public async Task<SubCategoryDto> GetByIdAsync(int id)
        {
            var SubCategory = await _context.SubCategories.AsNoTracking().FirstOrDefaultAsync(x => x.SubCategoryID == id);

            if (SubCategory == null)
            {
                throw new InvalidOperationException($"SubCategory with ID {id} not found.");
            }

            return _mapper.Map<SubCategoryDto>(SubCategory);
        }
        public async Task<List<SubCategoryDto>> GetByParentCategoryAsync(int parentCategoryId)
        {
            // استرجاع فئات فرعية بناءً على فئة الوالدين المحددة
            var subCategories = await _context.SubCategories
                .AsNoTracking()
                .Where(sc => sc.ParentCategoryID == parentCategoryId)
                .ToListAsync();

            // التحقق مما إذا كانت البيانات موجودة
            if (subCategories == null || !subCategories.Any())
            {
                throw new InvalidOperationException($"No subcategories found for ParentCategoryID {parentCategoryId}.");
            }

            // تحويل الكائنات إلى DTO
            var subCategoryDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);
            return subCategoryDtos;
        }


        public async Task<int?> CreateAsync(CreateSubCategoryDto dto)
        {
            var SubCategoryEntity = _mapper.Map<SubCategory>(dto);
            var validationErrors = ValidateObject(SubCategoryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create SubCategory: {string.Join(", ", validationErrors)}");
            }

            _context.SubCategories.Add(SubCategoryEntity);
            await _context.SaveChangesAsync();

            return SubCategoryEntity.SubCategoryID;
        }

        public async Task UpdateAsync(UpdateSubCategoryDto dto, int id)
        {
            var SubCategoryEntity = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryID == id);

            if (SubCategoryEntity == null)
            {
                throw new InvalidOperationException($"SubCategory with ID {id} not found.");
            }

            _mapper.Map(dto, SubCategoryEntity);

            var validationErrors = ValidateObject(SubCategoryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update SubCategory: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var SubCategoryEntity = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryID == id);

            if (SubCategoryEntity == null)
            {
                throw new InvalidOperationException($"SubCategory with ID {id} not found.");
            }
            _context.SubCategories.Remove(SubCategoryEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(SubCategory SubCategoryEntity)
        {
            var validationErrors = new List<string>();

            if (_context.SubCategories.Any(x => x.SubCategoryName == SubCategoryEntity.SubCategoryName && x.SubCategoryID != SubCategoryEntity.SubCategoryID))
            {
                validationErrors.Add("SubCategory Name Exists");
            }

            return validationErrors;
        }
    }
}
