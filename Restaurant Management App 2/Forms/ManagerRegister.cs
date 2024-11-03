using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_App_2
{
    public partial class ManagerRegister : Form
    {
        private MySqlConnection con;
        public ManagerRegister()
        {
            InitializeComponent();
            con = Connection.GetInstance().GetCon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDataAndRegister();
                this.Hide();
                new RestaurantRegister(txt_uname.Text, txt_password.Text).ShowDialog();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void CheckDataAndRegister()
        {
            //TODO create class (service maybe) for manager creation
            if (txt_uname.Text.Length == 0) throw new Exception("Please enter an username!");
            
            if (txt_password.Text.Length == 0) throw new Exception("Please enter a password!");
            if (txt_cpassword.Text.Length == 0) throw new Exception("Please enter a password!");

            if (txt_password.Text != txt_cpassword.Text) throw new Exception("Passwords do not match!");

            if (new MySqlCommand($"SELECT username FROM Managers WHERE username='{txt_uname.Text}'", con).ExecuteScalar() != null)
            { 
                throw new Exception("The username is already taken!"); 
            }

            new MySqlCommand($"INSERT INTO Managers(username, password) VALUES('{txt_uname.Text}','{txt_password.Text}')", con).ExecuteNonQuery();
        }
    }
}
