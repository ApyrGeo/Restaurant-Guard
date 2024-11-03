using MySql.Data.MySqlClient;
using Restaurant_Management_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2.Repository.old
{
    public class RepositoryProduct
    {
        protected static List<Product> product_list = new List<Product>();

        public List<Product> GetRepoList() { return product_list; }
        public Product FindProduct(int id)
        {
            return product_list.Find((p) => p.GetId() == id);
        }
        public Product FindProduct(string name)
        {
            return product_list.Find((p) => p.GetName() == name);

        }
        virtual public void AddToList(Product p) { product_list.Add(p); }
        virtual public void RemoveFromList(int id) { product_list.Remove(product_list.Find((p) => p.GetId() == id)); }
        virtual public void AddQuantity(int id, int quantity) { }

        virtual public void Refresh() { }
    }
    class RepositoryFileProduct : RepositoryProduct
    {
        private readonly Connection con;
        private readonly int rest_id;
        public RepositoryFileProduct(int rest_id) { this.rest_id = rest_id; con = new Connection(); LoadFromFile(); }
        public void LoadFromFile()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Products WHERE id_rest={rest_id}", con.GetCon());

            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                if (read.IsDBNull(4))
                    product_list.Add(new Product(read.GetInt32(0), read.GetString(2), read.GetString(3), read.GetInt32(5)));
                else
                    product_list.Add(new ConsumableProduct(read.GetInt32(0), read.GetString(2), read.GetString(3), read.GetInt32(4), read.GetInt32(5)));
            }

            con.Close();
        }
        override public void AddToList(Product p)
        {
            base.AddToList(p);

            con.Open();

            string cmd;
            if (p.GetType() == typeof(ConsumableProduct))
                cmd = $"INSERT INTO Products (id_rest,category,name,quantity, price) VALUES ({rest_id},'{p.GetCategory()}','{p.GetName()}',{p.GetQuantity()},{p.GetPrice()})";
            else
                cmd = $"INSERT INTO Products (id_rest,category,name, price)          VALUES ({rest_id},'{p.GetCategory()}','{p.GetName()}',{p.GetPrice()})";
            new MySqlCommand(cmd, con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Products','INSERT','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();
            con.Close();

            Refresh();
        }
        override public void RemoveFromList(int id)
        {
            base.RemoveFromList(id);

            con.Open();
            new MySqlCommand($"DELETE FROM Products WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Products','DELETE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }

        override public void AddQuantity(int id, int quantity)
        {
            Product wanted = product_list.Find((p) => p.GetId() == id);
            wanted.IncreaseQuantity(quantity);

            if (wanted.GetType() != typeof(ConsumableProduct)) return;

            con.Open();
            new MySqlCommand($"UPDATE Products SET quantity={wanted.GetQuantity()} WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Products','DELETE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }

        public override void Refresh()
        {
            product_list.Clear();
            LoadFromFile();
        }
    }
}
