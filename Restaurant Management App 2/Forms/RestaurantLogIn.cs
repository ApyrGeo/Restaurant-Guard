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


namespace Restaurant_Management_App
{
    public partial class RestaurantLogIn : Form
    {
        private Connection con;
        public RestaurantLogIn()
        {
            InitializeComponent();
            this.con = new Connection();
        }
        public RestaurantLogIn(int id)
        {
            InitializeComponent();
            this.con = new Connection();

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
                int id = CheckData();
                this.Hide();
                new MainMenu(new Restaurant(id)).ShowDialog();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        private int CheckData()
        {
            if (txt_id.Text.Length == 0) throw new Exception("Please enter an id!");
            foreach(char c in txt_id.Text)
            {
                if (!('0' <= c && c <= '9'))
                    throw new Exception("Invalid id!");
            }

            if (txt_password.Text.Length == 0) throw new Exception("Please enter a password!");

            con.Open();
            if (new MySqlCommand($"SELECT id FROM Restaurants WHERE id={Convert.ToInt32(txt_id.Text)} AND password='{txt_password.Text}'",con.GetCon()).ExecuteScalar() == null)
            {
                con.Close();
                throw new Exception("The given input does not correspond to any restaurant!");
            }
            int id = (int)new MySqlCommand($"SELECT id FROM Restaurants WHERE id={Convert.ToInt32(txt_id.Text)} AND password='{txt_password.Text}'", con.GetCon()).ExecuteScalar();
            con.Close();

            return id;
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
