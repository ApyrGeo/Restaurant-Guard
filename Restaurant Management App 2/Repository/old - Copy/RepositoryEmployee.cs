using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Restaurant_Management_App;

namespace Restaurant_Management_App_2.Repository.old
{
    public class RepositoryEmployee
    {
        protected static List<Employee> employee_list = new List<Employee>();

        public List<Employee> GetRepoList() { return employee_list; }
        public Employee FindEmployee(int id)
        {
            return employee_list.Find((e) => e.GetId() == id);
        }
        public Employee FindEmployee(string uname)
        {
            return employee_list.Find((e) => e.GetUsername() == uname);
        }
        virtual public void AddToList(Employee e) { employee_list.Add(e); }
        virtual public void RemoveFromList(int id) { employee_list.Remove(employee_list.Find((e) => e.GetId() == id)); }
        virtual public void Refresh() { }
    }
    class RepositoryFileEmployee : RepositoryEmployee
    {
        private readonly Connection con;
        private readonly int rest_id;
        public RepositoryFileEmployee(int rest_id) { this.rest_id = rest_id; con = new Connection(); LoadFromFile(); }
        public void LoadFromFile()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees WHERE id_rest={rest_id}", con.GetCon());

            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                employee_list.Add(new Employee(read.GetInt32(0), read.GetInt32(2), read.GetString(3), read.GetString(4), read.GetString(5)));
            }

            con.Close();
        }
        override public void AddToList(Employee e)
        {
            employee_list.Add(e);

            con.Open();
            new MySqlCommand($"INSERT INTO Employees (id_rest,salary,name, username, password) VALUES ({rest_id},{e.GetSalary()},'{e.GetName()}','{e.GetUsername()}','{e.GetPassword()}')", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Employees','INSERT','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        override public void RemoveFromList(int id)
        {
            employee_list.Remove(employee_list.Find((e) => e.GetId() == id));

            con.Open();
            new MySqlCommand($"DELETE FROM Employees WHERE id={id}", con.GetCon()).ExecuteNonQuery();
            new MySqlCommand($"INSERT INTO ChangeLog(id_rest,table_name,operation,timestamp) VALUES({rest_id},'Employees','DELETE','{DateTime.Now:yyyy-MM-dd HH:mm:ss}')", con.GetCon()).ExecuteNonQuery();

            con.Close();

            Refresh();
        }
        public override void Refresh()
        {
            employee_list.Clear();
            LoadFromFile();
        }
    }
}
