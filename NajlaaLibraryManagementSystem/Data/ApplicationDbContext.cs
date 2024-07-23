using Microsoft.EntityFrameworkCore;
using NajlaaLibraryManagementSystem.Dtos.Author;
using NajlaaLibraryManagementSystem.Dtos.Book;
using NajlaaLibraryManagementSystem.Models;

namespace NajlaaLibraryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Define DbSet properties for each entity
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }


        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.RentalPrice)
                    .HasColumnType("decimal(10,2)");

                //entity.Property(e => e.RetailPrice)
                //    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
