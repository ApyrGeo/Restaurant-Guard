using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository
{
    public class RepositoryTable
    {
        private readonly MySqlConnection _connection;
        private readonly int _restaurantId;
        public RepositoryTable(int restId) { 
            _connection = Connection.GetInstance().GetCon();
            _restaurantId = restId;
        }

        public List<Table> GetTables()
        {
            List<Table> tables = [];

            MySqlCommand cmd = new("SELECT id, seats, status, X, Y FROM Tables WHERE id_rest=@rid", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId);

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                tables.Add(new(reader.GetInt32(0), reader.GetInt32(1), reader.GetBoolean(2), reader.GetFloat(3), reader.GetFloat(4)));
            }

            return tables;
        }

        public Table GetTable(int id)
        {
            List<Table> tables = [];

            MySqlCommand cmd = new("SELECT id, seats, status, X, Y FROM Tables WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            return new Table(reader.GetInt32(0), reader.GetInt32(1), reader.GetBoolean(2), reader.GetFloat(3), reader.GetFloat(4));
        }

        public void AddTable(Table t)
        {
            MySqlCommand cmd = new("INSERT INTO Tables(id_rest, seats, status, X, Y) VALUES(@id_rest, @seats, @status, @X, @Y)", _connection);
            cmd.Parameters.AddWithValue("@id_rest", _restaurantId);
            cmd.Parameters.AddWithValue("@seats", t.GetNoSeats());
            cmd.Parameters.AddWithValue("@status", t.GetStatus());
            cmd.Parameters.AddWithValue("@X", t.GetX());
            cmd.Parameters.AddWithValue("@Y", t.GetY());

            cmd.ExecuteNonQuery();
        }

        public void RemoveTable(int id)
        {
            MySqlCommand cmd = new("DELETE FROM Tables WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void ChangeStatus(int id, bool newStatus)
        {
            MySqlCommand cmd = new("UPDATE TABLE Tables SET status=@nstatus WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nstatus", newStatus);

            cmd.ExecuteNonQuery();
        }
    }
}
