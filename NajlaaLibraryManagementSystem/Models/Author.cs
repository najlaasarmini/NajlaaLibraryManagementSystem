using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace NajlaaLibraryManagementSystem.Models
{
    public class Author
    {
        // Properties
        public int AuthorID { get; set; }

        [StringLength(100)] // Data Annotation not Fluent API // لأن الافتراضي بالانتيتي هو nvarchar(max)
        public string AuthorName { get; set; }

        // [DataType(DataType.Date)] // لأن الافتراضي بالإنتيتي هو datetime2
        public DateOnly? BirthDate { get; set; }

        public string? Biography { get; set; }

        // [ForeignKey("Country")]
        public int? NationalityID { get; set; }  // Reference property (foreign key property)

        public Country Country { get; set; } // Navigation Property

        public List<Book>? Books { get; set; } // Navigation Property


    }
}
