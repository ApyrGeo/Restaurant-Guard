using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2
{
    public class Employee
    {
        private string name, username, password;
        private int salary, id;

        public Employee(int id, int salary, string name, string username, string password)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
            this.username = username;
            this.password = password;
        }
        public int GetId () { return id; }
        public int GetSalary () {  return salary; }
        public string GetName () { return name; }
        public string GetUsername() { return username; }
        public string GetPassword () { return password; }
    }
}
