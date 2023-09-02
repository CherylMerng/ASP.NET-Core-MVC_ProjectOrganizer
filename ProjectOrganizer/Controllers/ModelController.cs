using Microsoft.AspNetCore.Mvc;
using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Controllers
{
    public class ModelController : Controller
    {
        // Add New Model /////////////////////////////////////////////////////////////////////////////////////
        public IActionResult CreateModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateModel(Model model) {
            return RedirectToAction("Dashboard", "Project");
        }

        // View Model /////////////////////////////////////////////////////////////////////////////////////
        // GetModelsByProjectId
        public IActionResult ViewModel(int projectId) 
        {
            return View();
        }
    }

}
