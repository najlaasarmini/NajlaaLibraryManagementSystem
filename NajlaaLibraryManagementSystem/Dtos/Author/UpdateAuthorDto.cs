using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public int AuthorID { get; set; }

        [StringLength(100)]
        public string? AuthorName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Biography { get; set; }

        public int? NationalityID { get; set; }
    }
}
