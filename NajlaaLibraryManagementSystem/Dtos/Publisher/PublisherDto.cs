using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.Publisher
{
    public class PublisherDto
    {
        public int PublisherID { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "PublisherName must be between 3 and 100 characters.")]
        public string PublisherName { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 200 characters.")]
        public string? Address { get; set; }

        [EmailAddress]
        [StringLength(50, MinimumLength = 15, ErrorMessage = "Email must be between 15 and 50 characters.")]
        public string? Email { get; set; }

        [StringLength(15, MinimumLength = 6, ErrorMessage = "Phone must be between 6 and 15 characters.")]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }
    }
}
