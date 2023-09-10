namespace ProjectOrganizer.Models.Project
{
    public class Property
    {
        public int Property_Id { get; set; }    // int -> default value 0
        
        public string Property_Name { get; set; } // green -> null - no default value

        public string Data_Type { get; set; }

        public string Access_Modifier { get; set;}

        public string? Description { get; set; }

        public int Model_Id { get; set; }   // foreign key
    }
}
