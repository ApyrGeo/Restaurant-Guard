using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository
{
    public class RepositoryCreateRestaurant
    {
        private MySqlConnection _connection;

        public RepositoryCreateRestaurant()
        {
            _connection = Connection.GetInstance().GetCon();
        }
        public List<Restaurant> GetExistingRestaurants()
        {
            MySqlCommand cmd = new("SELECT * FROM Restaurants");
            MySqlDataReader read = cmd.ExecuteReader();

            List<Restaurant> restaurants = new List<Restaurant>();

            while (read.Read())
            {
                restaurants.Add(new Restaurant(read.GetInt32(0), read.GetInt32(5), read.GetDouble(3), read.GetDouble(4), read.GetString(1), read.GetString(2)));
            }

            return restaurants;
        }

        public int AddRestaurant(Restaurant r)
        {
            MySqlCommand cmd = new("INSERT INTO Restaurants(name, password, L, W, id_manager) VALUES(@name, @password, @L, @W, @id_manager)", _connection);
            cmd.Parameters.AddWithValue("@name", r.GetName());
            cmd.Parameters.AddWithValue("@password", r.GetPassword());
            cmd.Parameters.AddWithValue("@L", r.GetLength());
            cmd.Parameters.AddWithValue("@W", r.GetWidth());
            cmd.Parameters.AddWithValue("@id_manager", r.GetManagerId());

            cmd.ExecuteNonQuery();

            return (int) new MySqlCommand("SELECT LAST_INSERT_ID() FROM Restaurants", _connection).ExecuteScalar();
        }

        public bool CheckManagerExists(int id_manager)
        {
            MySqlCommand cmd = new("SELECT username FROM Managers WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id_manager);

            MySqlDataReader reader = cmd.ExecuteReader();

            return reader.HasRows;
        }
    }
}
