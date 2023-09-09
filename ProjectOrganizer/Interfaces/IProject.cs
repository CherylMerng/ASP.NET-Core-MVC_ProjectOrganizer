using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Interfaces
{
    // controller <-> interface <-> repository <-> database
    // create constructor in controller to use interface

    // 1. To use interface -> copy method from repo
    public interface IProject
    {
        public bool CreateNewProject(string connectionString, Project newProject);
        public List<Project> GetAllProject(string connectionString);
        public Project GetProjectById(string connectionString, int id);
        public bool UpdateProject(string connectionString, Project project);
        public bool DeleteProjectById(string connectionString, int id);
    }
}
