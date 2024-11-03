using Restaurant_Management_App_2;
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
    public partial class Kitchen : Form
    {
        private ServiceOrder ServiceOrder;
        private ServiceProduct ServiceProduct;
        public Kitchen(ServiceOrder so, ServiceProduct sp)
        {
            InitializeComponent();
            this.TopLevel = false;
            ServiceOrder = so;
            ServiceProduct = sp;
            Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        public new void Refresh()
        {
            panel1.Controls.Clear();

            int x = 0, y = 0;
            foreach(Order o in GetServiceOrder().GetOrders("IN PROGRESS"))
            {
                FormOrder fo = new FormOrder(GetServiceProduct(), GetServiceOrder(), o);
                panel1.Controls.Add(fo);
                fo.TopLevel = false;
                fo.AutoScroll = true;
                fo.Show();
                fo.Location = new Point(x, y);

                if (x + 200 > this.Width)
                {
                    x = 0;
                    y += 600;
                }
                x += 200;
            }
        }
        private ServiceOrder GetServiceOrder() { return ServiceOrder; }
        private ServiceProduct GetServiceProduct() {  return ServiceProduct; }
        private void Kitchen_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
