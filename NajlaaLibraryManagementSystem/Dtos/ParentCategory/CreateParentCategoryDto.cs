using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.ParentCategory
{
    public class CreateParentCategoryDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "MainCategoryName must be between 2 and 100 characters.")]
        public string ParentCategoryName { get; set; }
    }
}
