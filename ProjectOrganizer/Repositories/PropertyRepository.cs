using Dapper;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;
using System.Data;
using System.Data.SqlClient;

namespace ProjectOrganizer.Repositories
{
    public class PropertyRepository : IProperty
    {
        public bool CreateProperty(string connectionString, Property newProperty) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Properties_StoredProcedures";
                var parameters = new
                {
                    Property_Id = 0,
                    PropertyName = newProperty.Property_Name,
                    DataType = newProperty.Data_Type,
                    AccessModifier = newProperty.Access_Modifier,
                    Description = newProperty.Description,
                    ModelId = @newProperty.Model_Id,

                    Process = "CreateProperty"
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
        }   // CreateProperty()

        public List<Property> GetAllPropertiesByModelId(string connectionString, int modelId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Models_StoredProcedures";
                var parameters = new
                {
                    PropertyId = 0,
                    PropertyName = "",
                    DataType = "",
                    AccessModifier = "",
                    Description = "",
                    ModelId = modelId,

                    Process = "GetPropertiesByModelId"
                };

                try
                {
                    var properties = connection.Query<Property>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    List<Property> result = properties.ToList();   // casting
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

        } // GetAllPropertiesByModelId
    }
}
