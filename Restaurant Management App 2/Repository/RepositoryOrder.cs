using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Restaurant_Management_App_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository 
{
    public class RepositoryOrder
    {
        private MySqlConnection _connection;
        private int _restaurantId;

        public RepositoryOrder(int restaurantId)
        {
            _connection = Connection.GetInstance().GetCon();
            _restaurantId = restaurantId;
        }

        public void AddOrder(Order o)
        {
            MySqlCommand cmd = new("INSERT INTO Orders(id_rest, status) VALUES(@rid, @status)", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId);
            cmd.Parameters.AddWithValue("@status", o.GetStatus());
            cmd.ExecuteNonQuery();

            int newId = (int)new MySqlCommand($"SELECT LAST_INSERTED_ID() FROM Orders ORDER BY id DESC WHERE id_rest={_restaurantId}", _connection).ExecuteScalar();

            o.GetCommand().ForEach( c => {
                cmd = new("INSERT INTO TableOrders(id_order, id_table, id_product, quantity) VALUES(@id_order, @id_table, @id_product, @quantity)", _connection);
                cmd.Parameters.AddWithValue("@id_order", newId);
                cmd.Parameters.AddWithValue("@id_table", o.GetIdTable());
                cmd.Parameters.AddWithValue("@id_product", c.Item1);
                cmd.Parameters.AddWithValue("@quantity", c.Item2);

                cmd.ExecuteNonQuery();
               });
        }

        public Order FindOrder(int id)
        {
            MySqlCommand cmd = new("SELECT status FROM Orders WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            string status = reader.GetString(0);

            cmd = new("SELECT id_table, id_product, quantity FROM TableOrders WHERE id_order=@oid", _connection);
            cmd.Parameters.AddWithValue("@oid", id);


            List<Tuple<int,int>> commands = new List<Tuple<int,int>>();
            int id_table = -1;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id_table = reader.GetInt32(0);
                commands.Add(new Tuple<int, int>(reader.GetInt32(1), reader.GetInt32(2)));
            }

            return new Order(id, id_table, commands, status);
        }

        public Order FindOrderByTableId(int table_id)
        {
            MySqlCommand cmd = new("SELECT id_order, id_product, quantity FROM TableOrders WHERE id_table=@tid", _connection);
            cmd.Parameters.AddWithValue("@tid", table_id);
            
            MySqlDataReader reader = cmd.ExecuteReader();
            
            List<Tuple<int, int>> commands = new List<Tuple<int, int>>();
            int id_order = -1;

            while (reader.Read())
            {
                id_order = reader.GetInt32(0);
                commands.Add(new(reader.GetInt32(1), reader.GetInt32(2)));
            }

            cmd = new("SELECT status FROM Orders WHERE id_order=@oid", _connection);
            cmd.Parameters.AddWithValue("@oid", id_order);

            string status = (string)cmd.ExecuteScalar();

            return new Order(id_order, table_id, commands, status);
        }
        public void RemoveOrder(int id)
        {
            MySqlCommand cmd = new("DELETE FROM Orders WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            cmd = new("DELETE FROM TableOrders WHERE id_order=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public void AddToOrder(int id, Tuple<int, int> item)
        {
            int id_table = (int)new MySqlCommand($"SELECT TOP 1 id_table FROM TableOrders WHERE id_order={id}", _connection).ExecuteScalar();

            MySqlCommand cmd = new("INSERT INTO TableOrders(id_order, id_table, id_product, quantity) VALUES(@id_order, @id_table, @id_product, @quantity)", _connection);
            cmd.Parameters.AddWithValue("@id_order", id);
            cmd.Parameters.AddWithValue("@id_table", id_table);
            cmd.Parameters.AddWithValue("@id_product", item.Item1);
            cmd.Parameters.AddWithValue("@quantity", item.Item2);

            cmd.ExecuteNonQuery();
        }

        public void ChangeStatus(int id, string nstatus)
        {
            MySqlCommand cmd = new("UPDATE TABLE Orders SET status=@status WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@status", nstatus);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public List<Order> GetOrders()
        {
            MySqlCommand cmd = new("SELECT id, status FROM Orders WHERE id_rest=@rid", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId); 

            List<Order> orders = new List<Order>();

            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                int id_order = read.GetInt32(0);
                string status = read.GetString(1);

                MySqlCommand subcmd = new("SELECT id_table, id_product, quantity FROM TableOrders WHERE id_order=@oid", _connection);
                subcmd.Parameters.AddWithValue("@oid", id_order);

                List<Tuple<int,int>> commands = new List<Tuple<int,int>>();
                int id_table = -1;

                MySqlDataReader read2 = subcmd.ExecuteReader();
                while (read2.Read())
                {
                    id_table = read2.GetInt32(0);
                    commands.Add(new(read2.GetInt32(1), read2.GetInt32(2)));
                }

                orders.Add(new(id_order,id_table, commands, status));
            }

            return orders;
        }
    }
}
