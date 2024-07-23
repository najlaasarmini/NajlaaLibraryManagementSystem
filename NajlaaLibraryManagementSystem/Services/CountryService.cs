using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.Country;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class CountryService : ICountryService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CountryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<CountryDto>> GetAllAsync()
        {
            var Countries = await _mapper.ProjectTo<CountryDto>(_context.Countries).ToListAsync();
            return Countries.AsQueryable();
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var Country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.CountryID == id);

            if (Country == null)
            {
                throw new InvalidOperationException($"Country with ID {id} not found.");
            }

            return _mapper.Map<CountryDto>(Country);
        }

        public async Task<int?> CreateAsync(CreateCountryDto dto)
        {
            var CountryEntity = _mapper.Map<Country>(dto);
            var validationErrors = ValidateObject(CountryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create Country: {string.Join(", ", validationErrors)}");
            }

            _context.Countries.Add(CountryEntity);
            await _context.SaveChangesAsync();

            return CountryEntity.CountryID;
        }

        public async Task UpdateAsync(UpdateCountryDto dto, int id)
        {
            var CountryEntity = await _context.Countries.FirstOrDefaultAsync(x => x.CountryID == id);

            if (CountryEntity == null)
            {
                throw new InvalidOperationException($"Country with ID {id} not found.");
            }

            _mapper.Map(dto, CountryEntity);

            var validationErrors = ValidateObject(CountryEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update Country: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var CountryEntity = await _context.Countries.FirstOrDefaultAsync(x => x.CountryID == id);

            if (CountryEntity == null)
            {
                throw new InvalidOperationException($"Country with ID {id} not found.");
            }
            _context.Countries.Remove(CountryEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(Country CountryEntity)
        {
            var validationErrors = new List<string>();

            if (_context.Countries.Any(x => x.CountryName == CountryEntity.CountryName && x.CountryID != CountryEntity.CountryID))
            {
                validationErrors.Add("Country Name Exists");
            }

            return validationErrors;
        }
    }
}
