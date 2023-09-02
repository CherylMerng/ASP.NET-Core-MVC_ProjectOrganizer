﻿using Dapper;
using ProjectOrganizer.Models;
using ProjectOrganizer.Models.Project;
using System.Data;
using System.Data.SqlClient;

namespace ProjectOrganizer.Repositories
{
    public class ModelRepository
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
        }
    }
}