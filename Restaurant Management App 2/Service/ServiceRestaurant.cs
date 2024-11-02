using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class ServiceRestaurant
    {
        private readonly Restaurant restaurant;
        private readonly RepositoryTable rt;
        public ServiceRestaurant(Restaurant restaurant, RepositoryTable rt)
        {
            this.rt = rt;
            this.restaurant = restaurant;
        }
        public Restaurant GetRestaurant() { return restaurant; }
        public RepositoryTable GetRepositoryTable() { return rt; }
        public ServiceRestaurant AddTable(double x, double y, int seats)
        {
            //MessageBox.Show(x.ToString() + " " + y.ToString());
            if (x < 0 || y < 0) throw new Exception("Invalid coordinates! Maybe the table is forced to go through a wall!");
            if (seats < 0) throw new Exception("Invalid number of seats!");

            GetRepositoryTable().AddToList(new Table(0, seats));
            restaurant.AddTable(new Tuple<double,double,int>(x, y, GetRepositoryTable().GetRepoList().Last().GetId()));

            return this;
        }
        public ServiceRestaurant RemoveTable(int table_id)
        {
            if (GetRepositoryTable().FindTable(table_id) == null) throw new Exception("Invalid table!");

            rt.RemoveFromList(table_id);
            restaurant.RemoveTable(table_id);
            return this;
        }
    }

}
