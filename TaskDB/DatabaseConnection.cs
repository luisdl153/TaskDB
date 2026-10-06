using System.Data.SqlClient;

namespace TaskDB
{
    public static class DatabaseConnection
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TaskDB.mdf;Integrated Security=True";
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}