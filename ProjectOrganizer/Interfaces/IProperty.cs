using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Interfaces
{
    public interface IProperty
    {
        public bool CreateProperty(string connectionString, Property newProperty);

        public List<Property> GetAllPropertiesByModelId(string connectionString, int modelId);

        public Property GetLatestPropertyByModelId(string connectionString, int modelId);

        public int DeletePropertyById(string connectionString, int propertyId);

        public Property GetPropertyById(string connectionString, int propertyId);
    }
}
