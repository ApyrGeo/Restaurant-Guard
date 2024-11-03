using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Restaurant_Management_App;

namespace Restaurant_Management_App_2.Repository.old
{
    public class RepositoryOrder
    {
        protected List<Order> list_order = new List<Order>();

        public virtual void AddToList(Order o)
        {
            list_order.Add(o);
        }
        public Order FindOrder(int id)
        {
            return list_order.Find((o) => o.GetId() == id);
        }
        public Order FindOrderByTableId(int table_id)
        {
            return list_order.Find((o) => o.GetIdTable() == table_id);
        }
        public virtual void RemoveFromList(int id)
        {
            list_order.Remove(FindOrder(id));
        }
        public virtual void AddToOrder(int id, Tuple<int, int> item)
        {
            FindOrder(id).AddToCommand(item);
        }
        public virtual void ChangeStatus(int id, string nstatus)
        {
            FindOrder(id).ChangeStatus(nstatus);
        }
        public List<Order> GetRepoList() { return list_order; }
        virtual public void Refresh() { }
    }
    public class RepositoryFileOrder : RepositoryOrder
    {
        private readonly Connection con;
        private readonly int rest_id;

        public RepositoryFileOrder(int rest_id)
        {
            this.rest_id = rest_id;
            con = new Connection();
            LoadFromFile();
        }
        public void LoadFromFile()
        {
            con.Open();
            MySqlDataReader read = new MySqlCommand($"SELECT * FROM Orders WHERE id_rest={rest_id}", con.GetCon()).ExecuteReader();
            while (read.Read())
            {
                List<Tuple<int, int>> command_list = new List<Tuple<int, int>>();
                string command = read.GetString(3);
                if (command.Length < 1) goto isEmpty;
                foreach (string s in command.Split(','))
                {
                    int id_product = Convert.ToInt32(s.Split('-')[0]);
                    int count = Convert.ToInt32(s.Split('-')[1]);

                    command_list.Add(new Tuple<int, int>(id_product, count));
                }
            isEmpty:
                base.AddToList(new Order(read.GetInt32(0), read.GetInt32(2), command_list, read.GetString(4)));
            }
            con.Close();
        }
        public override void AddToList(Order o)
        {
            base.AddToList(o);

            string command = "";
            foreach (Tuple<int, int> t in o.GetCommand())
            {
                command += t.Item1.ToString() + "-" + t.Item2.ToString() + ",";
            }
            if (command != "") command = command.Substring(0, command.Length - 1);

            con.Open();

            new MySqlCommand($"INSERT INTO Orders(id_rest, id_table, command, status) VALUES({rest_id},{o.GetIdTable()},'{command}', '{o.GetStatus()}')", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Orders','INSERT','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        public override void RemoveFromList(int id)
        {
            base.RemoveFromList(id);

            con.Open();
            new MySqlCommand($"DELETE FROM Orders WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Orders','DELETE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        public override void AddToOrder(int id, Tuple<int, int> item)
        {
            base.AddToOrder(id, item);

            Order wanted = FindOrder(id);

            string command = "";
            foreach (Tuple<int, int> t in wanted.GetCommand())
            {
                command += t.Item1.ToString() + "-" + t.Item2.ToString() + ",";
            }
            command = command.Substring(0, command.Length - 1);
            con.Open();
            new MySqlCommand($"UPDATE Orders SET command='{command}' WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Orders','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        public override void ChangeStatus(int id, string nstatus)
        {
            base.ChangeStatus(id, nstatus);

            con.Open();
            new MySqlCommand($"UPDATE Orders SET status='{nstatus}' WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Orders','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        public override void Refresh()
        {
            list_order.Clear();
            LoadFromFile();
        }
    }

}
