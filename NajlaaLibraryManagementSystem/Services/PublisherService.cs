using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.Publisher;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class PublisherService : IPublisherService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PublisherService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<PublisherDto>> GetAllAsync()
        {
            var Publishers = await _mapper.ProjectTo<PublisherDto>(_context.Publishers).ToListAsync();
            return Publishers.AsQueryable();
        }

        public async Task<PublisherDto> GetByIdAsync(int id)
        {
            var Publisher = await _context.Publishers.AsNoTracking().FirstOrDefaultAsync(x => x.PublisherID == id);

            if (Publisher == null)
            {
                throw new InvalidOperationException($"Publisher with ID {id} not found.");
            }

            return _mapper.Map<PublisherDto>(Publisher);
        }

        public async Task<int?> CreateAsync(CreatePublisherDto dto)
        {
            var PublisherEntity = _mapper.Map<Publisher>(dto);
            var validationErrors = ValidateObject(PublisherEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create Publisher: {string.Join(", ", validationErrors)}");
            }

            _context.Publishers.Add(PublisherEntity);
            await _context.SaveChangesAsync();

            return PublisherEntity.PublisherID;
        }

        public async Task UpdateAsync(UpdatePublisherDto dto, int id)
        {
            var PublisherEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.PublisherID == id);

            if (PublisherEntity == null)
            {
                throw new InvalidOperationException($"Publisher with ID {id} not found.");
            }

            _mapper.Map(dto, PublisherEntity);

            var validationErrors = ValidateObject(PublisherEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update Publisher: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var PublisherEntity = await _context.Publishers.FirstOrDefaultAsync(x => x.PublisherID == id);

            if (PublisherEntity == null)
            {
                throw new InvalidOperationException($"Publisher with ID {id} not found.");
            }
            _context.Publishers.Remove(PublisherEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(Publisher PublisherEntity)
        {
            var validationErrors = new List<string>();

            if (_context.Publishers.Any(x => x.PublisherName == PublisherEntity.PublisherName && x.PublisherID != PublisherEntity.PublisherID))
            {
                validationErrors.Add("Publisher Name Exists");
            }

            return validationErrors;
        }
    }
}
