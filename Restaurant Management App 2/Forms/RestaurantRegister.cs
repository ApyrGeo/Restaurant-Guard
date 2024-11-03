using MySql.Data.MySqlClient;
using Restaurant_Management_App_2.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App_2
{
    public partial class RestaurantRegister : Form
    {
        private MySqlConnection con;
        private ServiceCreateRestaurant scr;
        public RestaurantRegister()
        {
            InitializeComponent();
            con = Connection.GetInstance().GetCon();
            scr = new ServiceCreateRestaurant(new RepositoryCreateRestaurant());
        }
        public RestaurantRegister(string uname, string pass)
        {
            InitializeComponent();
            con = Connection.GetInstance().GetCon();
            txt_mname.Text = uname;
            txt_mpassword.Text = pass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = scr.AddRestaurant(txt_rname.Text, txt_rpassword.Text, txt_L.Text, txt_W.Text, txt_mname.Text, txt_mpassword.Text);

                MessageBox.Show($"Restaurant has been registered! The restaurant id is: {id}. Please remember it for further log in!.", "Attention");
                this.Hide();
                new RestaurantLogIn(new ServiceExistingRestaurant(new RepositoryExistingRestaurant(id))).ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void RestaurantRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
