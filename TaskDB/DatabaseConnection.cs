using System;
using System.Data.SqlClient;
using System.IO;

namespace TaskDB
{
    public static class DatabaseConnection
    {
        static DatabaseConnection()
        {
            string rutaProyecto = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")
            );

            AppDomain.CurrentDomain.SetData("DataDirectory", rutaProyecto);
        }

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