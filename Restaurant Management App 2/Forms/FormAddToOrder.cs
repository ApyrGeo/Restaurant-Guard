using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
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
    public partial class FormAddToOrder : Form
    {
        private ServiceOrder so;
        private ServiceProduct sp;
        private RestaurantFloor rf;
        private int order_id;
        public FormAddToOrder(ServiceOrder so, ServiceProduct sp, RestaurantFloor rf, int order_id)
        {
            InitializeComponent();
            this.so = so;
            this.sp = sp;
            this.rf = rf;
            this.order_id = order_id;

            Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= dataGridView1.Rows.Count) return;
            if (e.ColumnIndex != 4) return;

            string value = Interaction.InputBox("Enter quantity", "Input Dialog");
            if (value.Length == 0) MessageBox.Show("Please enter the quantity", "Error");
            else if (!value.All(char.IsDigit)) MessageBox.Show("Invalid Quantity!", "Error");
            else if (Convert.ToInt32(value) < 0) MessageBox.Show("Invalid Quantity!", "Error");


            try
            {
                so.AddCommandToOrder(order_id, new Tuple<int, int>(
                    Convert.ToInt32(dataGridView1[0, e.RowIndex].Value), Convert.ToInt32(value)
                    ));

                Refresh();

                rf.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private new void Refresh()
        {
            dataGridView1.Rows.Clear();
            foreach(Product p in sp.GetServiceRepo().GetRepoList())
            {
                dataGridView1.Rows.Add(p.GetId(), p.GetName(), p.GetCategory(), (p.GetQuantity() > 0 ? p.GetQuantity().ToString() : "-"));
            }
        }
        private void FormAddToOrder_Load(object sender, EventArgs e)
        {

        }

    }
}