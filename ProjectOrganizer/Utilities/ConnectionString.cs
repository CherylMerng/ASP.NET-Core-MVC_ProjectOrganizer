namespace ProjectOrganizer.Utilities
{
    public class ConnectionString
    {
        private IConfiguration _configuration;  // IConfiguration - built-in interface
        public static string connectionString;

        public ConnectionString(IConfiguration configuration) 
        { 
            _configuration = configuration;
            connectionString = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");  // from appsettings.json
        }

        public static string CName 
        { 
            get => connectionString;
        }

    }
}
