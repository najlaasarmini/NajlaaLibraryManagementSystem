using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.Language;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class LanguageService : ILanguageService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LanguageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<LanguageDto>> GetAllAsync()
        {
            var Languages = await _mapper.ProjectTo<LanguageDto>(_context.Languages).ToListAsync();
            return Languages.AsQueryable();
        }

        public async Task<LanguageDto> GetByIdAsync(int id)
        {
            var Language = await _context.Languages.AsNoTracking().FirstOrDefaultAsync(x => x.LanguageID == id);

            if (Language == null)
            {
                throw new InvalidOperationException($"Language with ID {id} not found.");
            }

            return _mapper.Map<LanguageDto>(Language);
        }

        public async Task<int?> CreateAsync(CreateLanguageDto dto)
        {
            var LanguageEntity = _mapper.Map<Language>(dto);
            var validationErrors = ValidateObject(LanguageEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create Language: {string.Join(", ", validationErrors)}");
            }

            _context.Languages.Add(LanguageEntity);
            await _context.SaveChangesAsync();

            return LanguageEntity.LanguageID;
        }

        public async Task UpdateAsync(UpdateLanguageDto dto, int id)
        {
            var LanguageEntity = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageID == id);

            if (LanguageEntity == null)
            {
                throw new InvalidOperationException($"Language with ID {id} not found.");
            }

            _mapper.Map(dto, LanguageEntity);

            var validationErrors = ValidateObject(LanguageEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update Language: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var LanguageEntity = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageID == id);

            if (LanguageEntity == null)
            {
                throw new InvalidOperationException($"Language with ID {id} not found.");
            }
            _context.Languages.Remove(LanguageEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(Language LanguageEntity)
        {
            var validationErrors = new List<string>();

            if (_context.Languages.Any(x => x.LanguageName == LanguageEntity.LanguageName && x.LanguageID != LanguageEntity.LanguageID))
            {
                validationErrors.Add("Language Name Exists");
            }

            return validationErrors;
        }
    }
}
