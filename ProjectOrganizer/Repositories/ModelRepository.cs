using Dapper;
using ProjectOrganizer.Interfaces;  // 2. To use interface -> implement
using ProjectOrganizer.Models;
using ProjectOrganizer.Models.Project;
using ProjectOrganizer.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace ProjectOrganizer.Repositories
{
    // 2. To use interface -> implement
    public class ModelRepository : IModel
    {
        public bool CreateModel(string connectionString, Model newModel) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                var procedure = "Models_StoredProcedures";
                var parameters = new
                {
                    ModelId = 0,
                    ModelName = newModel.Model_Name,
                    Description = newModel.Description,
                    ProjectId = newModel.Project_Id,

                    Process = "CreateModel"
                };

                try
                {             
                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        } // CreateModel()

        public List<Model> GetAllModelsByProjectId(string connectionString, int projectId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Models_StoredProcedures";
                var parameters = new
                {
                    ModelId = 0,
                    ModelName = "",
                    Description = "",
                    ProjectId = projectId,

                    Process = "GetModelsByProjectId"
                };

                try { 
                    var models = connection.Query<Model>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    List<Model> result = models.ToList();   // casting
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

        } // GetAllModelsByProjectId()

        public bool DeleteModelById(string connectionString, int modelId) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Models_StoredProcedures";
                var parameters = new
                {
                    ModelId = modelId,
                    ModelName = "",
                    Description = "",
                    ProjectId = 0,

                    Process = "DeleteModelById"
                };

                try
                {
                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

        }   // DeleteModelById()

        public Model GetModelById(string connectionString, int modelId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Models_StoredProcedures";
                var parameters = new
                {
                    ModelId = modelId,
                    ModelName = "",
                    Description = "",
                    ProjectId = 0,

                    Process = "GetModelById"
                };

                try
                {
                    Model model = connection.QuerySingle<Model>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return model;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }   // GetModelById

        }

    }
}
