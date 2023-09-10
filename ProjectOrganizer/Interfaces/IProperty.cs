using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Interfaces
{
    public interface IProperty
    {
        public bool CreateProperty(string connectionString, Property newProperty);
        public List<Property> GetAllPropertiesByModelId(string connectionString, int modelId);
    }
}
