using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository
{
    public class RepositoryProduct
    {
        private readonly MySqlConnection _connection;
        private readonly int _restaurantId;

        public RepositoryProduct(int rid)
        {
            _connection = Connection.GetInstance().GetCon();
            _restaurantId = rid;
        }

        public void AddProduct(Product p)
        {
            MySqlCommand cmd = new("INSERT INTO Products(id_rest, category, name, quantity, price) VALUES(@id_rest, @category, @name, @quantity, @price)", _connection);
            cmd.Parameters.AddWithValue("@id_rest", _restaurantId);
            cmd.Parameters.AddWithValue("@category", p.GetCategory());
            cmd.Parameters.AddWithValue("@name", p.GetName());
            cmd.Parameters.AddWithValue("@quantity", typeof(ConsumableProduct) == p.GetType() ? p.GetQuantity() : null);
            cmd.Parameters.AddWithValue("@price", p.GetPrice());

            cmd.ExecuteNonQuery();
        }

        public List<Product> GetProducts()
        {
            List<Product> products = [];

            MySqlCommand cmd = new("SELECT id, category, name, price, quantity FROM Products WHERE id_rest=@rid", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId);

            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read()) 
            {
                if (reader.IsDBNull(4))
                    products.Add(new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                else
                    products.Add(new ConsumableProduct(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
            }

            return products;
        }

        public Product GetProduct(int id)
        {
            MySqlCommand cmd = new("SELECT id, category, name, price, quantity FROM Products WHERE id=@pid", _connection);
            cmd.Parameters.AddWithValue("@pid", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            return reader.IsDBNull(4) ?
                new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)) :
                new ConsumableProduct(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));
        }
        public Product GetProduct(string name)
        {
            MySqlCommand cmd = new("SELECT id, category, name, price, quantity FROM Products WHERE name=@name", _connection);
            cmd.Parameters.AddWithValue("@name", name);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            return reader.IsDBNull(4) ?
                new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)) :
                new ConsumableProduct(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));
        }

        public void RemoveProduct(int id)
        {
            MySqlCommand cmd = new("DELETE FROM Products WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);


            cmd.ExecuteNonQuery();
        }

        public void AddQuantity(int id, int quantity)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT quantity FROM Products WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            if (cmd.ExecuteScalar() == null) return;

            int crtQuantity = (int) cmd.ExecuteScalar();

            crtQuantity += quantity;

            cmd = new MySqlCommand("UPDATE TABLE Products SET quantity=@nqty WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@nqty", crtQuantity);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
