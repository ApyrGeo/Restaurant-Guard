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

        public int AddRestaurant(string name, string password, string L, string W, string mname, string mpass)
        {
            if (name.Length == 0) throw new Exception("Please enter a restaurant name!");

            if (password.Length == 0) throw new Exception("Please enter a restaurant password!");

            if (L.Length == 0) throw new Exception("Please enter the Length of the restaurant!");
            if (Convert.ToDouble(L) < 0) throw new Exception("Please enter a valid Length!");

            if (W.Length == 0) throw new Exception("Please enter the Width of the restaurant!");
            if (Convert.ToDouble(L) < 0) throw new Exception("Please enter a valid Width!");

            if (mname.Length == 0) throw new Exception("Please enter an username!");
            if (mpass.Length == 0) throw new Exception("Please enter a password!");

            int id_manager = repo.CheckManagerExists(mname, mpass);
            if (id_manager < 0) throw new Exception("Manager does not exist!");

            int id = repo.AddRestaurant(new Restaurant(0, id_manager, Convert.ToDouble(L), Convert.ToDouble(W), name, password));

            return id;
        }
    }
}
