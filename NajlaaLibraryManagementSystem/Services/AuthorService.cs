using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Data;
using NajlaaLibraryManagementSystem.Dtos.Author;
using NajlaaLibraryManagementSystem.Models;
using NajlaaLibraryManagementSystem.Services.Interfaces;

namespace NajlaaLibraryManagementSystem.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<AuthorDto>> GetAllAsync()
        {
            var Authors = await _mapper.ProjectTo<AuthorDto>(_context.Authors).ToListAsync();
            return Authors.AsQueryable();
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var Author = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.AuthorID == id);

            if (Author == null)
            {
                throw new InvalidOperationException($"Author with ID {id} not found.");
            }

            return _mapper.Map<AuthorDto>(Author);
        }

        public async Task<int?> CreateAsync(CreateAuthorDto dto)
        {
            var AuthorEntity = _mapper.Map<Author>(dto);
            var validationErrors = ValidateObject(AuthorEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to create Author: {string.Join(", ", validationErrors)}");
            }

            _context.Authors.Add(AuthorEntity);
            await _context.SaveChangesAsync();

            return AuthorEntity.AuthorID;
        }

        public async Task UpdateAsync(UpdateAuthorDto dto, int id)
        {
            var AuthorEntity = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorID == id);

            if (AuthorEntity == null)
            {
                throw new InvalidOperationException($"Author with ID {id} not found.");
            }

            _mapper.Map(dto, AuthorEntity);

            var validationErrors = ValidateObject(AuthorEntity);

            if (validationErrors.Any())
            {
                throw new InvalidOperationException($"Failed to update Author: {string.Join(", ", validationErrors)}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var AuthorEntity = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorID == id);

            if (AuthorEntity == null)
            {
                throw new InvalidOperationException($"Author with ID {id} not found.");
            }
            _context.Authors.Remove(AuthorEntity);
            await _context.SaveChangesAsync();
        }

        private List<string> ValidateObject(Author AuthorEntity)
        {
            var validationErrors = new List<string>();

            if (_context.Authors.Any(x => x.AuthorName == AuthorEntity.AuthorName && x.AuthorID != AuthorEntity.AuthorID))
            {
                validationErrors.Add("Author Name Exists");
            }

            return validationErrors;
        }
    }
}
