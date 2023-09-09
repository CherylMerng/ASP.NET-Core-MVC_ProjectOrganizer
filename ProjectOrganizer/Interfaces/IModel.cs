using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Interfaces
{
    public interface IModel
    {
        // 1. To use interface -> copy method from repo
        public bool CreateModel(string connectionString, Model newModel);
        public List<Model> GetAllModelsByProjectId(string connectionString, int projectId);
        public bool DeleteModelById(string connectionString, int modelId);
        public Model GetModelById(string connectionString, int modelId);
    }
}