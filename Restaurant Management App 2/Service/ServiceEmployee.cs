using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant_Management_App_2.Repository;

namespace Restaurant_Management_App_2
{
    public class ServiceEmployee
    {
        private readonly RepositoryEmployee repo;
        public ServiceEmployee(RepositoryEmployee repo)
        {
            this.repo = repo;
        }
        public ServiceEmployee AddEmployee(string salary, string name, string username, string password)
        {
            if (name.Length == 0) throw new Exception("Please enter a name!");
            if (username.Length == 0) throw new Exception("Please enter a username!");
            if (password.Length == 0) throw new Exception("Please enter a password!");
            if (salary.Length == 0) throw new Exception("Please enter a salary!");
            if (!salary.All(char.IsDigit)) throw new Exception("Invalid salary!");
            if (!(0 < Convert.ToInt32(salary))) throw new Exception("Invalid salary amount!");
            if (GetServiceRepo().FindEmployee(username) != null) throw new Exception("Username already exists!");

            GetServiceRepo().SaveEmployee(new Employee(0, Convert.ToInt32(salary), name, username, password));

            return this;
        }
        public ServiceEmployee DeleteEmployee(int id)
        { 
            if (GetServiceRepo().GetEmployee(id) == null)
                throw new Exception("Invalid id");
            GetServiceRepo().DeleteEmployee(id);

            return this;
        }
        public Employee? GetEmployee(string uname, string password)
        {
            foreach(Employee e in GetServiceRepo().GetEmployees())
            {
                if (e.GetUsername() == uname && e.GetPassword() == password)
                    return e;
            }
            return null;
        }
        public RepositoryEmployee GetServiceRepo() 
        { 
            return repo; 
        }
    }
}
