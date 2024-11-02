using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class Connection
    {
        private readonly string connectionString = File.ReadAllText("..\\..\\..\\.env");
        public MySqlConnection connection;

        public Connection()
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Open() { connection.Open(); }
        public void Close() { connection.Close(); }
        public MySqlConnection GetCon() { return connection; }
    }

}
