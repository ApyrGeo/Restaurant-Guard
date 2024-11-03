using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using Restaurant_Management_App_2.Syncronization;

namespace Restaurant_Management_App_2.Repository
{ 
    public class RepositoryEmployee
    {
        private MySqlConnection _connection;
        private int _idRestaurant;
        public RepositoryEmployee(int idRestaurant) {
            _connection = Connection.GetInstance().GetCon();
            _idRestaurant = idRestaurant;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            MySqlCommand cmd = new MySqlCommand("SELECT id, salary, name, username, password FROM Employees WHERE id_rest=@idr", _connection);
            cmd.Parameters.AddWithValue("@idr", _idRestaurant);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
            }
            reader.Close();
            return employees;
        }

        public Employee GetEmployee(int id) 
        {

            MySqlCommand cmd = new MySqlCommand("SELECT id, salary, name, username, password FROM Employees WHERE id_rest=@idr AND id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@idr", _idRestaurant);
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            Employee e = new(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            reader.Close();
            return e;
        }

        public void SaveEmployee(Employee e)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Employees(id_rest, salary, name, username, password) VALUES(@id_rest, @salary, @name, @username, @password)", _connection);
            cmd.Parameters.AddWithValue("@id_rest", _idRestaurant);
            cmd.Parameters.AddWithValue("@salary",e.GetSalary());
            cmd.Parameters.AddWithValue("@name", e.GetName());
            cmd.Parameters.AddWithValue("@username", e.GetUsername());
            cmd.Parameters.AddWithValue("@password", e.GetPassword());

            cmd.ExecuteNonQuery();

            new ChangeLog(_idRestaurant).InsertIntoChangLog("Employees", Operation.INSERT);
        }

        public void DeleteEmployee(int id)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM Employees WHERE id=@id", _connection);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            new ChangeLog(_idRestaurant).InsertIntoChangLog("Employees", Operation.DELETE);
        }

        public Employee FindEmployee(string username)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT id, salary, name, username, password FROM Employees WHERE id_rest=@idr AND username=@username", _connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@idr", _idRestaurant);
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            
            Employee e = new(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            reader.Close();
            return e;
        }
    }
}
