using Microsoft.AspNetCore.Mvc;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;
using ProjectOrganizer.Utilities;

namespace ProjectOrganizer.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProject _projectInterface;
        private readonly IModel _modelInterface;
        private readonly IConfiguration _configuration;
        public static string connectionString;

        // Constructor
        public ProjectController (IProject projectInterface, IModel modelInterface, IConfiguration configuration)
        {
            _projectInterface = projectInterface;
            _modelInterface = modelInterface;
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");  // from appsettings.json
        }

        //////////////////// View Dashboard /////////////////////////////////////////////////////////////////////////////////////////
            // IActionResult - return page or action (Controller to View), need to return
        public IActionResult Dashboard()
        {
            // show project list - create list and pass to view
                // to send data from Controller to View - 1. View Model 2. View Bag (ASP.Net Core dictionary)
            List<Project> projectList = _projectInterface.GetAllProject(connectionString);
            ViewBag.ProjectList = projectList;  // ViewBag.viewbagKey = viewbagValue
            return View();
        }

        //////////////////// Create New Project /////////////////////////////////////////////////////////////////////////////////////
        // need 2 methods for form submission
        // 1) go to form page
        public IActionResult CreateProject() 
        { 
            return View();        
        }

        // 2) after submission, go to another page
            // need model binding to send object (connect with form and page)
                // parameter's data type -> modelName
                // ** why need data binding -> submit form as an object
        [HttpPost]
        public IActionResult CreateProject(Project projectForm) 
        {
            Project newProject = new Project();

            newProject.Project_Name = projectForm.Project_Name; // propertyName in model
            newProject.Description = projectForm.Description;
            newProject.Created_Date = projectForm.Created_Date;
            newProject.Project_Status = true;

            // Test without connecting with database
             // projectList.Add(newProject);
             // List index 0 - newProject obj1, index 1 - newProject obj2 

            bool check = _projectInterface.CreateNewProject(connectionString, newProject);

            return RedirectToAction("Dashboard", "Project");    // go to another page after form submission
            // for controller to work - go to controller first
            // (controllerMethod, controllerName)
        }

        //////////////////// Get Project Details /////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult GetProjectById(int id)
        {
            Project project = _projectInterface.GetProjectById(connectionString, id);
            List<Model> models = _modelInterface.GetAllModelsByProjectId(connectionString, id);

            if (project != null) {
                ViewBag.Project = project;  // Project => name (key), project => data (value)

                if (models != null) 
                { 
                    ViewBag.Models = models;
                }

                return View("ProjectDetails");  // need this () if page name is different with controller method
            }
            return RedirectToAction("Dashboard", "Project");

        }

        //////////////////// Update Project /////////////////////////////////////////////////////////////////////////////////////////
            // need 2 methods 

        public IActionResult UpdateProject(int id) {
            Project project = _projectInterface.GetProjectById(connectionString, id);
            if (project != null) {
                ViewBag.Project = project;
                return View("UpdateProject");
            }
            return RedirectToAction("Dashboard", "Project");
        }

        [HttpPost]
        public IActionResult UpdateProject(Project projectForm)
        {
            bool check = _projectInterface.UpdateProject(connectionString, projectForm);
            return RedirectToAction("GetProjectById", "Project", new { id = projectForm.Project_Id });
        }

        //////////////////// Delete Project /////////////////////////////////////////////////////////////////////////////////////////

        public IActionResult DeleteProjectById(int id)
        {
            bool check = _projectInterface.DeleteProjectById(connectionString, id);
            return RedirectToAction("Dashboard");
        }

    }
}
