namespace NajlaaLibraryManagementSystem.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        // مالها لزوم .. بس انه
        public List<Author>? Authors { get; set; } // Navigation Property

    }
}
