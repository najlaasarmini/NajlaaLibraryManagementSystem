using NajlaaLibraryManagementSystem.Dtos.SubCategory;
using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.ParentCategory
{
    public class ParentCategoryDto
    {
        public int ParentCategoryID { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "MainCategoryName must be between 2 and 100 characters.")]
        public string ParentCategoryName { get; set; }

        public List<SubCategoryDto>? SubCategories { get; set; }
    }
}
