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
    public partial class EmployeeLogIn : Form
    {
        private ServiceEmployee se;
        private MainMenu mmenu;
        public EmployeeLogIn(ServiceEmployee se, MainMenu mmenu)
        {
            InitializeComponent();
            this.se = se;
            this.mmenu = mmenu;
        }

        private void EmployeeLogIn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (se.GetEmployee(txt_uname.Text, txt_password.Text) != null)
            {
                mmenu.UnlockEmployee();
                mmenu.UnlockKitchen();
                this.Close();
                this.Dispose();
            }
            else MessageBox.Show("Invalid user credentials!","Error");
            
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
