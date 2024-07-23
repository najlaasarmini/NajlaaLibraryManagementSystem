using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "SubCategoryName must be between 2 and 100 characters.")]
        public string SubCategoryName { get; set; }

        public int ParentCategoryID { get; set; }

        public ParentCategory ParentCategory { get; set; }

        public List<Book>? Books { get; set; } // Navigation Property

    }
}
