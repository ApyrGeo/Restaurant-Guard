using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Restaurant_Management_App_2;
using Restaurant_Management_App_2.Syncronization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            MySqlCommand cmd = new("INSERT INTO Orders(id_rest, status, id_table) VALUES(@rid, @status, @id_table)", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId);
            cmd.Parameters.AddWithValue("@status", o.GetStatus());
            cmd.Parameters.AddWithValue("@id_table", o.GetIdTable());
            cmd.ExecuteNonQuery();

            UInt64 newId = (UInt64)new MySqlCommand($"SELECT LAST_INSERT_ID() FROM Orders WHERE id_rest={_restaurantId}", _connection).ExecuteScalar();

            o.GetCommand().ForEach( c => {
                cmd = new("INSERT INTO TableOrders(id_order, id_product, quantity) VALUES(@id_order, @id_product, @quantity)", _connection);
                cmd.Parameters.AddWithValue("@id_order", Convert.ToInt32(newId));
                cmd.Parameters.AddWithValue("@id_product", c.Item1);
                cmd.Parameters.AddWithValue("@quantity", c.Item2);

                cmd.ExecuteNonQuery();
               });

            new ChangeLog(_restaurantId).InsertIntoChangLog("Orders", Operation.INSERT);
        }

        public Order FindOrder(int id)
        {
            MySqlCommand cmd = new("SELECT status, id_table FROM Orders WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            string status = reader.GetString(0);
            int id_table = reader.GetInt32(1);
            reader.Close();

            cmd = new("SELECT id_product, quantity FROM TableOrders WHERE id_order=@oid", _connection);
            cmd.Parameters.AddWithValue("@oid", id);


            List<Tuple<int,int>> commands = new List<Tuple<int,int>>();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                commands.Add(new Tuple<int, int>(reader.GetInt32(0), reader.GetInt32(1)));
            }

            reader.Close();
            return new Order(id, id_table, commands, status);
        }

        public Order FindOrderByTableId(int table_id)
        {
            MySqlCommand cmd = new("SELECT status, id FROM Orders WHERE id_table=@tid", _connection);
            cmd.Parameters.AddWithValue("@tid", table_id);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            string status = reader.GetString(0);
            int id_order = reader.GetInt32(1);
            reader.Close();


            cmd = new("SELECT id_product, quantity FROM TableOrders WHERE id_order=@oid", _connection);
            cmd.Parameters.AddWithValue("@oid", id_order);
            
            reader = cmd.ExecuteReader();
            
            List<Tuple<int, int>> commands = new List<Tuple<int, int>>();

            while (reader.Read())
            {
                commands.Add(new(reader.GetInt32(0), reader.GetInt32(1)));
            }
            reader.Close();

            return new Order(id_order, table_id, commands, status);
        }
        public void RemoveOrder(int id)
        {
            MySqlCommand cmd = new("DELETE FROM TableOrders WHERE id_order=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            cmd = new("DELETE FROM Orders WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            new ChangeLog(_restaurantId).InsertIntoChangLog("Orders", Operation.DELETE);
        }
        public void AddToOrder(int id, Tuple<int, int> item)
        {
            MySqlCommand cmd = new("INSERT INTO TableOrders(id_order, id_product, quantity) VALUES(@id_order, @id_product, @quantity)", _connection);
            cmd.Parameters.AddWithValue("@id_order", id);
            cmd.Parameters.AddWithValue("@id_product", item.Item1);
            cmd.Parameters.AddWithValue("@quantity", item.Item2);

            cmd.ExecuteNonQuery();

            new ChangeLog(_restaurantId).InsertIntoChangLog("Orders", Operation.INSERT);
        }

        public void ChangeStatus(int id, string nstatus)
        {
            MySqlCommand cmd = new("UPDATE Orders SET status=@status WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@status", nstatus);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            new ChangeLog(_restaurantId).InsertIntoChangLog("Orders", Operation.UPDATE);
        }
        public List<Order> GetOrders()
        {
            MySqlCommand cmd = new("SELECT id, status, id_table FROM Orders WHERE id_rest=@rid", _connection);
            cmd.Parameters.AddWithValue("@rid", _restaurantId); 

            List<Order> orders = new List<Order>();

            MySqlDataReader read = cmd.ExecuteReader();

            List<Tuple<int, string, int>> values = new List<Tuple<int, string, int>>(); // id_order/status/id_table
            while (read.Read())
            {
                int id_order = read.GetInt32(0);
                string status = read.GetString(1);
                int id_table = read.GetInt32(2);
                values.Add(new(id_order, status, id_table));
            }
            read.Close();

            values.ForEach(v => {
                MySqlCommand subcmd = new("SELECT id_product, quantity FROM TableOrders WHERE id_order=@oid", _connection);
                subcmd.Parameters.AddWithValue("@oid", v.Item1);

                List<Tuple<int, int>> commands = new List<Tuple<int, int>>();

                MySqlDataReader read2 = subcmd.ExecuteReader();
                while (read2.Read())
                {
                    commands.Add(new(read2.GetInt32(0), read2.GetInt32(1)));
                }
                read2.Close();
                orders.Add(new(v.Item1, v.Item3, commands, v.Item2));
            }); 
            return orders;
        }
    }
}
