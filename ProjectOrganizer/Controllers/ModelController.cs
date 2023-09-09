using Microsoft.AspNetCore.Mvc;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModel _modelInterface;
        private readonly IConfiguration _configuration;
        public static string connectionString;

        // Constructor
        public ModelController(IModel modelInterface, IConfiguration configuration)
        {
            _modelInterface = modelInterface;
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");  // from appsettings.json
        }

        //////////////////// Manage Model /////////////////////////////////////////////////////////////////////////////////////
        // GetModelsByProjectId
        // first time running, call this method -> empty form
        public IActionResult ManageModels(int id)
        {
            // id => need to be same in routing -> Program.cs
            List<Model> models = _modelInterface.GetAllModelsByProjectId(connectionString, id);
            ViewBag.Models = models;
            ViewBag.ProjectId = id;
            return View();
        }

        // after updating, call this method
        [HttpPost]
        public IActionResult ManageModels(Model modelForm)
        {
            Model newModel = new Model();
            newModel.Model_Name = modelForm.Model_Name;     // sometimes want to set value here
            newModel.Description = modelForm.Description;
            newModel.Project_Id = modelForm.Project_Id;

            bool check = _modelInterface.CreateModel(connectionString, newModel);

            return RedirectToAction("ManageModels", "Model", new { id = modelForm.Project_Id});
            // As the first method (ManageModels()) accepts parameter, need to pass parameter in second method
        }

        //////////////////// Delete Model /////////////////////////////////////////////////////////////////////////////////////
        public IActionResult DeleteModel(int id)    // model id
        {
            Model model = _modelInterface.GetModelById(connectionString, id);
            int projectId = model.Project_Id;
            bool check = _modelInterface.DeleteModelById(connectionString, id); 
            return RedirectToAction("ManageModels", "Model", new { id = projectId });
        }
    }

}