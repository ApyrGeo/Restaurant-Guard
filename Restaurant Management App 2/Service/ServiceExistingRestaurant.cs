using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_App_2.Repository;

namespace Restaurant_Management_App_2
{
    public class ServiceExistingRestaurant
    {
        private RepositoryExistingRestaurant repo;
        public ServiceExistingRestaurant(RepositoryExistingRestaurant repo)
        {
            this.repo = repo;
        }
        public void CheckCredentials(string id, string password)
        {
            if (id.Length == 0) throw new Exception("Please enter an id!");
            foreach (char c in id)
            {
                if (!('0' <= c && c <= '9'))
                    throw new Exception("Invalid id!");
            }

            if (password.Length == 0) throw new Exception("Please enter a password!");

            if (!(repo.GetRestaurant().GetId() == Convert.ToInt32(id) && repo.GetRestaurant().GetPassword() == password))
                throw new Exception("Id and password do not match");
        }

        public Restaurant GetRestaurant() => repo.GetRestaurant();

        public void SetId(int id)
        { 
            if(repo.GetId() == -1)
                repo.SetId(id);
        }
    }

}
