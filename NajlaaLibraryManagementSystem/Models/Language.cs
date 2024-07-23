using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Models
{
    public class Language
    {
        public int LanguageID { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "LanguageName must be between 4 and 50 characters.")]
        public string LanguageName { get; set; }

        public List<Book>? Books { get; set; } // Navigation Property

    }
}
