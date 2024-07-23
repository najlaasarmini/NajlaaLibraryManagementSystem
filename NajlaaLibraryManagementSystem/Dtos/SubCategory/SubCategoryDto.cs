using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.SubCategory
{
    public class SubCategoryDto
    {
        public int SubCategoryID { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "SubCategoryName must be between 2 and 100 characters.")]
        public string SubCategoryName { get; set; }

        public int ParentCategoryID { get; set; }
    }
}
