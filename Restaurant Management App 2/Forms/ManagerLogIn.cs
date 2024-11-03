using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Restaurant_Management_App_2
{
    public partial class ManagerLogIn : Form
    {
        private MainMenu mmenu;
        private MySqlConnection con;
        public ManagerLogIn(MainMenu mmenu)
        {
            InitializeComponent();
            this.mmenu = mmenu;
            con = Connection.GetInstance().GetCon();
        }

        private void ManagerLogIn_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(new MySqlCommand($"SELECT id FROM Managers WHERE username='{txt_uname.Text}' AND password='{txt_password.Text}'",con).ExecuteScalar() != null)
            {
                mmenu.UnlockAdmin();

                this.Close();
                this.Dispose();
            }
            else MessageBox.Show("Invalid user credentials!", "Error");
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            label5.BackColor = Color.IndianRed;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.BackColor = this.BackColor;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
