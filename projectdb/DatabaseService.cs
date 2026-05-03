using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace projectdb
{
    public class DatabaseService
    {


        string kamal = "Server=DESKTOP-9VOLK8E;Database=project;Trusted_Connection=True;TrustServerCertificate=True;";
        string badwy = @"Data Source=MAHMOUDBADWY\MSSQLSERVER01;Initial Catalog=project;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=""SQL Server Management Studio"";Command Timeout=0";
        string gerges = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=project;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=""SQL Server Management Studio"";Command Timeout=2147483647";

        private string GetConnectionS()
        {
            return badwy;
        }

        public SqlConnection GetConnection()
        {
            string _connectionString = GetConnectionS();
            return new SqlConnection(_connectionString);
        }

        // Helper to test connectivity and return an error message if it fails.
        public string TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    conn.Close();
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : string.Empty);
            }
        }
    }
}
