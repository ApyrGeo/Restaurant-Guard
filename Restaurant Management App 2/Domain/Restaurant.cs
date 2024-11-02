using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Org.BouncyCastle.Ocsp;
using Google.Protobuf.WellKnownTypes;

namespace Restaurant_Management_App
{
    public class Restaurant
    {
        private  Connection con;
        private int id, manager_id;
        private double L, W;
        private string name;
        private List<Tuple<double, double, int>> tables;

        public Restaurant(int id)
        {
            con = new Connection();
            this.id = id;
            LoadFromFile();
        }
        private void LoadFromFile()
        {
            con.Open();
            MySqlDataReader read = new MySqlCommand($"SELECT * FROM Restaurants WHERE id={id}", con.GetCon()).ExecuteReader();
            read.Read();
            this.L = read.GetDouble(3);
            this.W = read.GetDouble(4);
            this.name = read.GetString(1);
            this.manager_id = read.GetInt32(6);
            string tables_string = read.GetString(5);
            con.Close();
            this.tables = new List<Tuple<double, double, int>>();

            if (tables_string.Length == 0) goto empty;
            foreach (string table in tables_string.Split('/'))
            {
                tables.Add(new Tuple<double, double, int>(
                    Convert.ToDouble(table.Split('-')[0]),
                    Convert.ToDouble(table.Split('-')[1]),
                    Convert.ToInt32(table.Split('-')[2])));
            }
        empty:
            return;
        }
        public int GetId() { return id; }
        public double GetLength() { return L; }
        public double GetWidth() {  return W; }
        public int GetManagerId() {  return manager_id; }
        public string GetName() { return name; }
        public List<Tuple<double, double,int>> GetTables() {  return tables; }

        public void AddTable(Tuple<double, double, int> table) 
        { 
            tables.Add(table);

            con.Open();
            string tbl = "";
            if (tables.Count == 0) goto isEmpty;
            foreach(Tuple<double,double,int> tb in tables)
            {
                tbl += tb.Item1.ToString() + "-" + tb.Item2.ToString() + "-" +tb.Item3.ToString() + "/";
            }
            tbl = tbl.Substring(0, tbl.Length - 1);

            new MySqlCommand($"UPDATE Restaurants SET tables='{tbl}' WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({id},'Restaurants','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

        isEmpty:
            con.Close();
        }
        public void ChangeLength(double L)
        {
            this.L = L;

            con.Open();
            
            new MySqlCommand($"UPDATE Restaurants SET L={L} WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({id},'Restaurants','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();
            con.Close();
        }
        public void ChangeWidth(double W)
        {
            this.W = W;

            con.Open();
            new MySqlCommand($"UPDATE Restaurants SET W={W} WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation) VALUES({id},'Restaurants','UPDATE')", con.GetCon()).ExecuteNonQuery();
            con.Close();
        }
        public void Refresh()
        {
            LoadFromFile();
        }

        public void RemoveTable(int table_id)
        {
            tables.Remove(tables.Find((Tuple<double, double, int> t) => t.Item3 == table_id));

            con.Open();
            string tbl = "";
            if (tables.Count == 0) goto isEmpty2;
            foreach (Tuple<double, double, int> tb in tables)
            {
                tbl += tb.Item1.ToString() + "-" + tb.Item2.ToString() + "-" + tb.Item3.ToString() + "/";
            }
            tbl = tbl.Substring(0, tbl.Length - 1);
        isEmpty2:

            
            new MySqlCommand($"UPDATE Restaurants SET tables='{tbl}' WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({id},'Restaurants','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

        
            con.Close();
        }
    }
}
