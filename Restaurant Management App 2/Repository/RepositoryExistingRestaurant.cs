using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository
{
    public class RepositoryExistingRestaurant
    {
        private MySqlConnection _connection;
        private int _restaurantId = -1;

        public RepositoryExistingRestaurant(int id_rest)
        {
            _connection = Connection.GetInstance().GetCon();
            _restaurantId = id_rest;
        }
        public RepositoryExistingRestaurant()
        {
            _connection = Connection.GetInstance().GetCon();
        }
        public int GetId() => _restaurantId;
        public void SetId(int id)
        {
            _restaurantId = id;
        }
        public Restaurant GetRestaurant()
        {
            MySqlCommand cmd = new("SELECT name, password, L, W, id_manager FROM Restaurants WHERE id=@rid", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId);

            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();

            return new Restaurant(_restaurantId, read.GetInt32(4), read.GetDouble(2), read.GetDouble(3), read.GetString(0), read.GetString(1));
        }
    }
}
