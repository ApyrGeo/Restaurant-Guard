using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class RepositoryTable
    {
        protected List<Table> list_tables =  new List<Table>();
        public virtual void AddToList(Table t) { list_tables.Add(t); }
        public virtual void RemoveFromList(int id) { list_tables.Remove(FindTable(id)); }
        public List<Table> GetRepoList() { return list_tables;}
        public Table FindTable(int id) { return list_tables.Find((Table t) => t.GetId() == id); }
        public virtual void ChangeStatus(int id, bool status) { FindTable(id).ChangeStatus(status); }
        public virtual void Refresh() { }
    }
    class RepositoryFileTable: RepositoryTable
    {
        private Connection con;
        private int rest_id;
        public RepositoryFileTable(int rest_id) 
        {
            this.rest_id = rest_id;
            con = new Connection();
            LoadFromFile();
        }
        public void LoadFromFile()
        {
            con.Open();

            MySqlDataReader read = new MySqlCommand($"SELECT * FROM Tables WHERE id_rest={rest_id}",con.GetCon()).ExecuteReader();
            while(read.Read())
            {
                list_tables.Add(new Table(read.GetInt32(0), read.GetInt32(2), read.GetBoolean(3)));
            }
            con.Close();
        }
        public override void AddToList(Table t)
        {
            base.AddToList(t);

            con.Open();
            new MySqlCommand($"INSERT INTO Tables (id_rest,seats,status) VALUES ({rest_id},{t.GetNoSeats()},{t.GetStatus()})", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Tables','INSERT','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();
            con.Close();

            Refresh();
        }
        public override void RemoveFromList(int id)
        {
            base.RemoveFromList(id);

            con.Open();
            new MySqlCommand($"DELETE FROM Tables WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Tables','DELETE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();
            con.Close();

            Refresh();

        }
        public override void ChangeStatus(int id, bool status)
        {
            base.ChangeStatus(id, status);

            con.Open();
            new MySqlCommand($"UPDATE Tables SET status = {status} WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Tables','UPDATE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();
            con.Close();
            Refresh();
        }
        public override void Refresh()
        {
            list_tables.Clear();
            LoadFromFile();
        }
    }
}
