using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Syncronization
{
    public class ChangeLog
    {
        private MySqlConnection _connection;
        private int _restaurantId;

        public ChangeLog(int rid) 
        { 
            _restaurantId = rid;
            _connection = Connection.GetInstance().GetCon();
        }
        private string GetOperation(Operation operation)
        {
            switch (operation)
            {
                case Operation.INSERT: return "INSERT";
                case Operation.DELETE: return "DELETE";
                case Operation.UPDATE: return "UPDATE";
                default: return "";
            }

        }
        public void InsertIntoChangLog(string tableName, Operation operation)
        {
            MySqlCommand cmd = new("INSERT INTO ChangeLog(id_rest, table_name, operation, timestamp) VALUES(@id_rest, @table_name, @operation, @timestamp)", _connection);
            cmd.Parameters.AddWithValue("@id_rest", _restaurantId);
            cmd.Parameters.AddWithValue("@table_name", tableName);
            cmd.Parameters.AddWithValue("@operation", GetOperation(operation));
            cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            cmd.ExecuteNonQuery();
        }
    }
}
