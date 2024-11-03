using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using Restaurant_Management_App_2.Repository;

namespace Restaurant_Management_App_2
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RestaurantLogIn(new ServiceExistingRestaurant(new RepositoryExistingRestaurant())));
        }
    }
}
