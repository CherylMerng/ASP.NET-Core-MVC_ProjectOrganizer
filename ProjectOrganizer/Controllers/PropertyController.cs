using Microsoft.AspNetCore.Mvc;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;

namespace ProjectOrganizer.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IProperty _propertyInterface;
        private readonly IConfiguration _configuration;
        public static string connectionString;

        // Constructor
        public PropertyController(IProperty propertyInterface, IConfiguration configuration)
        {
            _propertyInterface = propertyInterface;
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");  // from appsettings.json
        }

        public IActionResult ManageProperties(int id)
        {
            List<Property> properties = _propertyInterface.GetAllPropertiesByModelId(connectionString, id);
            ViewBag.Properties = properties;
            ViewBag.ModelId = id;
            return View();
        }

        /* [HttpPost]
        public IActionResult AddNewProperty(Property propertyForm)
        {
            Property newProperty = new Property();
            newProperty.Property_Name = propertyForm.Property_Name;
            newProperty.Data_Type = propertyForm.Data_Type;
            newProperty.Access_Modifier = propertyForm.Access_Modifier;
            newProperty.Description = propertyForm.Description;
            newProperty.Model_Id = propertyForm.Model_Id;

            bool check = _propertyInterface.CreateProperty(connectionString, newProperty);

            return RedirectToAction("ManageProperties", "Property", new { id = propertyForm.Model_Id });
        }*/

        [HttpPost]
        public ActionResult AddNewProperty(Property property) 
        {
            Property newProperty = new Property();
            newProperty.Property_Name = property.Property_Name;
            newProperty.Data_Type = property.Data_Type;
            newProperty.Access_Modifier = property.Access_Modifier;
            newProperty.Description = property.Description;
            newProperty.Model_Id = property.Model_Id;

            bool check = _propertyInterface.CreateProperty(connectionString, newProperty);

            Property latestProperty = _propertyInterface.GetLatestPropertyByModelId(connectionString, property.Model_Id);

            return Json(latestProperty);
        }
        // Way 1 - send object -> Property - newProperty 
        // Way 2 - send the whole list -> need to clear the existing data
        // Way 3 - return property id after creating an object/ inserting one row

        // Delete Property ////////////////////////////////////////////////////////////////////
        [HttpPost]
        public ActionResult DeleteProperty(int id)
        {
            Property property = _propertyInterface.GetPropertyById(connectionString, id);
            int modelId = property.Model_Id;

            int check = _propertyInterface.DeletePropertyById(connectionString, id);

            if (check == 1)
            {
                List<Property> properties = _propertyInterface.GetAllPropertiesByModelId(connectionString, modelId);
                return Json(properties);
            }
            else 
            {
                return null;
            }
        }
    }
}