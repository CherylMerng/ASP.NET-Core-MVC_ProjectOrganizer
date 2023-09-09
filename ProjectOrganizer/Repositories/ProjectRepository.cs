using Dapper;
using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Models.Project;  // 2. To use interface -> implement
using ProjectOrganizer.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectOrganizer.Repositories
{
    // controller <-> interface <-> repository <-> database
    // repo - access db, CRUD db table

    // 2. To use interface -> implement
    public class ProjectRepository : IProject
    {
        public bool CreateNewProject(string connectionString, Project newProject)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Projects_StoredProcedures";

                var parameters = new
                {
                    // need to write all parameters in stored procedure
                    ProjectId = 0,  // default value
                    ProjectName = newProject.Project_Name,
                    Description = newProject.Description,
                    CreatedDate = newProject.Created_Date,
                    ProjectStatus = newProject.Project_Status,

                    Process = "CreateNewProject"
                };

                try 
                {
                    // Execute => Dapper method - run only, no return type (Create, Update, Delete)
                    connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return true;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }   // CreateNewProject()


        // 1 row = 1 object => list
        public List<Project> GetAllProject(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))  // dapper
            {
                var procedure = "Projects_StoredProcedures";

                var parameters = new
                {
                    // need to write all parameters in stored procedure
                    ProjectId = 0,  // default value
                    ProjectName = "",
                    Description = "",
                    CreatedDate = "",
                    ProjectStatus = "",

                    Process = "GetAllProject"
                };

                try 
                {
                    // Query => Dapper method - retrieve more than one row in db
                        // Query<Project> - need to define return type
                    var projectList = connection.Query<Project>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    List <Project> result = projectList.ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
        }   // GetAllProject()


        // only 1 object
        public Project GetProjectById (string connectionString, int id) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                var procedure = "Projects_StoredProcedures";

                var parameters = new
                {
                    ProjectId = id,
                    ProjectName = "",
                    Description = "",
                    CreatedDate = "",
                    ProjectStatus = "",

                    Process = "GetProjectById"
                };

                try
                {
                    // QuerySingle => Dapper method - retrieve one row in db
                    Project project = connection.QuerySingle<Project>(procedure, parameters, commandType: CommandType.StoredProcedure);
                    return project;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.ToString());
                     return null;
                }
            }
        }   // GetProjectById


        public bool UpdateProject(string connectionString, Project project) {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Projects_StoredProcedures";

                var parameters = new
                {
                    ProjectId = project.Project_Id,
                    ProjectName = project.Project_Name,
                    Description = project.Description,
                    CreatedDate = project.Created_Date,
                    ProjectStatus = project.Project_Status,

                    Process = "UpdateProject"
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

        }   // UpdateProject()

        public bool DeleteProjectById(string connectionString, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var procedure = "Projects_StoredProcedures";

                var parameters = new
                {
                    ProjectId = id,
                    ProjectName = "",
                    Description = "",
                    CreatedDate = "",
                    ProjectStatus = "",

                    Process = "DeleteProjectById"
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
        }   // DeleteProjectById


        // ------------------------ Using Queries ---------------------------

        /*
        public bool CreateNewProject(string connectionString, Project newProject)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO Projects(Project_Name, Description, Created_Date, Project_Status) " +
                            "VALUES(@ProjectName, @Description, @CreatedDate, @ProjectStatus)";

                var parameters = new
                {
                    ProjectName = newProject.Project_Name,
                    Description = newProject.Description,
                    CreatedDate = newProject.Created_Date,
                    ProjectStatus = newProject.Project_Status
                };

                try
                {
                    connection.Execute(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }   // CreateNewProject()


        // 1 row = 1 object => list
        public List<Project> GetAllProject(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))  // dapper
            {
                var query = "SELECT * FROM Projects";

                try
                {
                    var projectList = connection.Query<Project>(query);
                    List<Project> result = projectList.ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
        }   // GetAllProject()


        // only 1 object
        public Project GetProjectById(string connectionString, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Projects WHERE Project_Id = @Id";

                var parameter = new
                {
                    Id = id,
                };

                try
                {
                    // QuerySingle => Dapper method - retrieve one row in db
                    Project project = connection.QuerySingle<Project>(query, parameter);
                    return project;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
        }   // GetProjectById


        public bool UpdateProject(string connectionString, Project project)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE Projects SET Project_Name = @ProjectName, Description = @Description, Created_Date = @CreatedDate, Project_Status = @ProjectStatus " +
                    "WHERE Project_Id = @ProjectId";

                var parameters = new
                {
                    ProjectId = project.Project_Id,
                    ProjectName = project.Project_Name,
                    Description = project.Description,
                    CreatedDate = project.Created_Date,
                    ProjectStatus = project.Project_Status
                };

                try
                {
                    connection.Execute(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

        }   // UpdateProject()

        public bool DeleteProjectById(string connectionString, int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM Projects WHERE Project_Id = @ProjectId";

                var parameter = new
                {
                    ProjectId = id,
                };

                try
                {
                    Project project = connection.QuerySingle<Project>(query, parameter);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }   // DeleteProjectById

        */

    }
}