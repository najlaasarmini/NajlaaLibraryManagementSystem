using System.ComponentModel.DataAnnotations;

namespace NajlaaLibraryManagementSystem.Dtos.Country
{
    public class UpdateCountryDto
    {
        public int CountryId { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "CountryName must be between 2 and 100 characters.")]
        public string? CountryName { get; set; }
    }
}
