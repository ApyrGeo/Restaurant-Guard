using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Restaurant_Management_App_2
{
    public partial class ManageProducts : Form
    {
        private ServiceProduct sp;
        private MainMenu mmenu;
        public ManageProducts(ServiceProduct sp, MainMenu mmenu)
        {
            InitializeComponent();
            this.sp = sp;
            this.mmenu = mmenu;

            dataGridView1.Height = Convert.ToInt32(Convert.ToDouble(mmenu.GetPanelSize().Height) * 3.00 / 5.00);

            Refresh();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
        }
        public new void Refresh()
        {
            dataGridView1.Rows.Clear();
            foreach(Product p in sp.GetServiceRepo().GetRepoList())
            {
                dataGridView1.Rows.Add(p.GetId().ToString(), p.GetName(), p.GetCategory(),(p.GetQuantity() > 0 ? p.GetQuantity().ToString() : "-"), p.GetPrice());                
            
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= dataGridView1.Rows.Count - 1) return;

            if(e.ColumnIndex == 5) // Add Quantity
            {
                if (dataGridView1[3, e.RowIndex].Value.ToString() == "-") return;
                string value;
                while(true)
                {
                    value = Interaction.InputBox("Enter quantity", "Input Dialog");
                    if (value.Length == 0) MessageBox.Show("Please enter the quantity", "Error");
                    else if (!value.All(char.IsDigit)) MessageBox.Show("Invalid Quantity!", "Error");
                    else if (Convert.ToInt32(value) < 0) MessageBox.Show("Invalid Quantity!", "Error");
                    else break;
                }
                sp.AddQuantity(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value), Convert.ToInt32(value));

                Refresh();
            }
            if(e.ColumnIndex == 6) // Remove Product
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to remove this product?", "Warning", MessageBoxButtons.YesNo);
                if(dr == DialogResult.Yes)
                {
                    sp.DeleteProduct(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value));
                    Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                  
               
                if(txt_qty.Text == "" || txt_qty.Text == "-")
                    sp.AddProduct(txt_name.Text, txt_cat.Text, txt_price.Text, "");
                else
                    sp.AddProduct(txt_name.Text, txt_cat.Text, txt_price.Text, txt_qty.Text);


                txt_cat.Text = "";
                txt_price.Text = "";
                txt_qty.Text = "-";
                txt_name.Text = "";
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ManageProducts_ResizeEnd(object sender, EventArgs e)
        {
            dataGridView1.Dock= DockStyle.Fill;
            dataGridView1.Height = Height - 500;
            dataGridView1.Width = Width;
        }
    }
}
