using MySql.Data.MySqlClient;
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
        public RestaurantRegister()
        {
            InitializeComponent();
            con = Connection.GetInstance().GetCon();
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
                int id = CheckDataAndRegister();

                MessageBox.Show($"Restaurant has been registered! The restaurant id is: {id}. Please remember it for further log in!.", "Attention");
                this.Hide();
                new RestaurantLogIn(id).ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private int CheckDataAndRegister()
        {
            if (txt_rname.Text.Length == 0) throw new Exception("Please enter a restaurant name!");

            if (txt_rpassword.Text.Length == 0) throw new Exception("Please enter a restaurant password!");

            if (txt_L.Text.Length == 0) throw new Exception("Please enter the Length of the restaurant!");
            if (Convert.ToInt32(txt_L.Text) < 0) throw new Exception("Please enter a valid Length!");

            if (txt_W.Text.Length == 0) throw new Exception("Please enter the Width of the restaurant!");
            if (Convert.ToInt32(txt_L.Text) < 0) throw new Exception("Please enter a valid Width!");

            if (txt_mname.Text.Length == 0) throw new Exception("Please enter an username!");
            if (txt_mpassword.Text.Length == 0) throw new Exception("Please enter a password!");

            if (new MySqlCommand($"SELECT username FROM Managers WHERE username='{txt_mname.Text}'", con).ExecuteScalar() == null)
            {
                throw new Exception("Manager does not exist!");
            }

            if (new MySqlCommand($"SELECT id FROM Managers WHERE username='{txt_mname.Text}' AND password='{txt_mpassword.Text}'", con).ExecuteScalar() == null)
            {

                throw new Exception("Incorrect password!");
            }
            int id_manager = (int)new MySqlCommand($"SELECT id FROM Managers WHERE username='{txt_mname.Text}' AND password='{txt_mpassword.Text}'", con).ExecuteScalar();

            new MySqlCommand($"INSERT INTO Restaurants(name,password,L,W,tables,id_manager) " +
                $"VALUES('{txt_rname.Text}','{txt_rpassword.Text}',{Convert.ToDouble(txt_L.Text)},{Convert.ToDouble(txt_W.Text)},'',{id_manager})", con).ExecuteNonQuery();


            int id = (int)new MySqlCommand($"SELECT id FROM Restaurants WHERE name='{txt_rname.Text}' AND password='{txt_rpassword.Text}'", con).ExecuteScalar();

            return id;
        }

        private void RestaurantRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
