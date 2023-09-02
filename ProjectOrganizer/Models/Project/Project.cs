// all models need CRUD - will use Ado.net

namespace ProjectOrganizer.Models.Project
{
    public class Project
    {
        // properties
        public int Project_Id { get; set; }

        public string Project_Name { get; set; }

        public string? Description { get; set; }

        public DateTime? Created_Date { get; set; }

        public bool Project_Status { get; set; }
        // false for non-active
    }
}
