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
    public partial class FormOrder : Form
    {
        private Order order;
        private ServiceProduct sp;
        private ServiceOrder so;
        public FormOrder(ServiceProduct sp, ServiceOrder so, Order order)
        {
            InitializeComponent();
            this.order = order;
            this.sp = sp;
            this.so = so;

            this.TopLevel = false;
            CreateOrder();
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            
        }
        private void CreateOrder()
        {
            this.Width = 200;
            this.Height = 50 * order.GetCommand().Count() + 50;

            foreach(var item in order.GetCommand())
            {
                lbl.Text += $"{sp.GetServiceRepo().GetProduct(item.Item1).GetName()} x {item.Item2}\n";
            }
        }

        private void FormOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            so.ChangeStatus(order.GetId(), "DONE");
            this.Dispose();
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
