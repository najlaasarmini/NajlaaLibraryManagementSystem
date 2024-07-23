using NajlaaLibraryManagementSystem.Dtos.Author;
using NajlaaLibraryManagementSystem.Dtos.Language;
using NajlaaLibraryManagementSystem.Dtos.Publisher;
using NajlaaLibraryManagementSystem.Dtos.SubCategory;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.Book
{
    public class BookDto
    {
        public int BookID { get; set; }

        [StringLength(255, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 255 characters.")]
        public string Title { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "ISBN must be between 2 and 20 characters.")]
        public string? ISBN { get; set; }

        public int? Pages { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Summary { get; set; }
        public string? CoverImageURL { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "BookEdition must be between 5 and 50 characters.")]
        public string? BookEdition { get; set; }

        public int? AvailableCopies { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? WholesalePrice { get; set; }

        public decimal? RetailPrice { get; set; }
        public decimal? RentalPrice { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? AuthorID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? LanguageID { get; set; }
        public int? PublisherID { get; set; }
        public AuthorDto? Author { get; set; }
        public SubCategoryDto? SubCategory { get; set; }
        public LanguageDto? Language { get; set; }
        public PublisherDto? Publisher { get; set; }
    }
}
