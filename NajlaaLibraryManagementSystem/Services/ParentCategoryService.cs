using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.ParentCategory;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class ParentCategoryService : IParentCategoryService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ParentCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<ParentCategoryDto>> GetAllAsync()
        {
            var ParentCategories = await _mapper.ProjectTo<ParentCategoryDto>(_context.ParentCategories).ToListAsync();
            return ParentCategories.AsQueryable();
        }

        public async Task<ParentCategoryDto> GetByIdAsync(int id)
        {
            var ParentCategory = await _context.ParentCategories.AsNoTracking().FirstOrDefaultAsync(x => x.ParentCategoryID == id);

            if (ParentCategory == null)
            {
                throw new InvalidOperationException($"ParentCategory with ID {id} not found.");
            }

            return _mapper.Map<ParentCategoryDto>(ParentCategory);
        }

        public async Task<int?> CreateAsync(CreateParentCategoryDto dto)
        {
            var ParentCategoryEntity = _mapper.Map<ParentCategory>(dto);
            var validationErrors = ValidateObject(ParentCategoryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create ParentCategory: {string.Join(", ", validationErrors)}");
            }

            _context.ParentCategories.Add(ParentCategoryEntity);
            await _context.SaveChangesAsync();

            return ParentCategoryEntity.ParentCategoryID;
        }

        public async Task UpdateAsync(UpdateParentCategoryDto dto, int id)
        {
            var ParentCategoryEntity = await _context.ParentCategories.FirstOrDefaultAsync(x => x.ParentCategoryID == id);

            if (ParentCategoryEntity == null)
            {
                throw new InvalidOperationException($"ParentCategory with ID {id} not found.");
            }

            _mapper.Map(dto, ParentCategoryEntity);

            var validationErrors = ValidateObject(ParentCategoryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update ParentCategory: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ParentCategoryEntity = await _context.ParentCategories.FirstOrDefaultAsync(x => x.ParentCategoryID == id);

            if (ParentCategoryEntity == null)
            {
                throw new InvalidOperationException($"ParentCategory with ID {id} not found.");
            }
            _context.ParentCategories.Remove(ParentCategoryEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(ParentCategory ParentCategoryEntity)
        {
            var validationErrors = new List<string>();

            if (_context.ParentCategories.Any(x => x.ParentCategoryName == ParentCategoryEntity.ParentCategoryName && x.ParentCategoryID != ParentCategoryEntity.ParentCategoryID))
            {
                validationErrors.Add("ParentCategory Name Exists");
            }

            return validationErrors;
        }
    }
}
