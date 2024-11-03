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
    public partial class RestaurantLogIn : Form
    {
        private MySqlConnection _connection;
        private ServiceExistingRestaurant ser;
        public RestaurantLogIn(ServiceExistingRestaurant ser)
        {
            InitializeComponent();
            _connection = Connection.GetInstance().GetCon();
            this.ser = ser;
        }
        public RestaurantLogIn(ServiceExistingRestaurant ser, int id)
        {
            InitializeComponent();
            _connection = Connection.GetInstance().GetCon();
            this.ser = ser;
            txt_id.Text = id.ToString();
        }
        private void RestaurantLogIn_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_register_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lbl_register_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ser.CheckCredentials(txt_id.Text, txt_password.Text);
                ser.SetId(Convert.ToInt32(txt_id.Text));
                this.Hide();
                new MainMenu(ser.GetRestaurant()).ShowDialog();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void lbl_register_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you a registered manager?", "Choose", MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                this.Hide();
                new RestaurantRegister().ShowDialog();
                this.Close();
            }
            else if(dr == DialogResult.No)
            {
                this.Hide();
                new ManagerRegister().ShowDialog();
                this.Close();
            }
        }
    }
}
