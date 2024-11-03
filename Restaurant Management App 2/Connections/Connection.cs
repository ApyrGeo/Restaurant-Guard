using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2
{
    public sealed class Connection
    {
        private static Connection? instance = null;
        private static readonly string connectionString = File.ReadAllText("..\\..\\..\\.env");
        private MySqlConnection connection;

        private Connection()
        {
        } 

        public static Connection GetInstance() {
            if (instance == null)
            {
                instance = new Connection();
                instance.connection = new MySqlConnection(connectionString);
                instance.connection.Open();
            }

            return instance;
        }
        public MySqlConnection GetCon() { return connection; }
    }

}
