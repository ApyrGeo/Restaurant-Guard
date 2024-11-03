using Restaurant_Management_App_2.Repository;
using Restaurant_Management_App_2;

namespace Restaurant_Management_App_2
{
    public class ServiceCreateRestaurant
    {
        private RepositoryCreateRestaurant repo;

        public ServiceCreateRestaurant(RepositoryCreateRestaurant repo)
        {
            this.repo = repo;
        }

        public int AddRestaurant(string name, string password, double L, double W, int id_manager)
        {
            if (!repo.CheckManagerExists(id_manager)) throw new Exception("Manager does not exist!");
            if (name == "") throw new Exception("Please enter a name!");
            if (password == "") throw new Exception("Please enter a password!");

            int id = repo.AddRestaurant(new Restaurant(0, id_manager, L, W, name, password));

            return id;
        }
    }
}
