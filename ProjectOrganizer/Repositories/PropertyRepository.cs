using Dapper;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

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
                    PropertyId = 0,
                    PropertyName = newProperty.Property_Name,
                    DataType = newProperty.Data_Type,
                    AccessModifier = newProperty.Access_Modifier,
                    Description = newProperty.Description,
                    ModelId = @newProperty.Model_Id,

                    Process = "CreateProperty"
                };

                // Execute => return true (1) or false (0)
                try
                {
                    var check = connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
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
                var procedure = "Properties_StoredProcedures";
                var parameters = new
                {
                    PropertyId = 0,
                    PropertyName = "",
                    DataType = "",
                    AccessModifier = "",
                    Description = "",
                    ModelId = modelId,

                    Process = "GetAllPropertiesByModelId"
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

        public Property GetLatestPropertyByModelId(string connectionString, int modelId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Properties_StoredProcedures";
                var parameters = new
                {
                    PropertyId = 0,
                    PropertyName = "",
                    DataType = "",
                    AccessModifier = "",
                    Description = "",
                    ModelId = modelId,

                    Process = "GetLatestProperty"
                };

                try
                {
                    Property result = connection.QuerySingle<Property>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

        } // GetLatestPropertyByModelId

        public int DeletePropertyById(string connectionString, int propertyId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Properties_StoredProcedures";
                var parameters = new
                {
                    PropertyId = propertyId,
                    PropertyName = "",
                    DataType = "",
                    AccessModifier = "",
                    Description = "",
                    ModelId = "",

                    Process = "DeletePropertyById"
                };

                try
                {
                    int check = connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return check;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return 0;
                }
            }

        }   // DeletePropertyById

        public Property GetPropertyById(string connectionString, int propertyId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Properties_StoredProcedures";
                var parameters = new
                {
                    PropertyId = propertyId,
                    PropertyName = "",
                    DataType = "",
                    AccessModifier = "",
                    Description = "",
                    ModelId = 0,

                    Process = "GetPropertyById"
                };

                try
                {
                    Property result = connection.QuerySingle<Property>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }

        } // GetPropertyById

    }
}