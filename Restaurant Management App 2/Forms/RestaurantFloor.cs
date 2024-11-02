using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Restaurant_Management_App
{
    public partial class RestaurantFloor : Form
    {
        private MainMenu mmenu;
        private ServiceTable st;
        private ServiceRestaurant sr;
        private ServiceOrder so;
        private ServiceProduct sp;
        private int L, H, x, y, dy;
        private int ImgScale;
        private int current_table;

        private void contextMenuStrip_empty_Opening(object sender, CancelEventArgs e)
        {

        }
        private int getXPositionByPerc(double percentage)
        {
            return Convert.ToInt32(percentage * Convert.ToInt32(mmenu.GetPanelSize().Width) / 100.00);
        }
        private int getYPositionByPerc(double percentage)
        {
            return Convert.ToInt32(percentage * Convert.ToInt32(mmenu.GetPanelSize().Height) / 100.00);
        }
        private double getPercentageWidth(int x)
        {
            double res = Convert.ToDouble(x) * 100.00 / Convert.ToDouble(mmenu.GetPanelSize().Width);
            return Math.Round(res, 2);
        }
        private double getPercentagehHeight(int y)
        {
            double res = Convert.ToDouble(y) * 100.00 / Convert.ToDouble(mmenu.GetPanelSize().Height);
            return Math.Round(res, 2);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                double actualX = e.Location.X - ImgScale / 2;
                double actualY = e.Location.Y - ImgScale / 2;

                foreach (var t in sr.GetRestaurant().GetTables())
                {
                    int x = getXPositionByPerc(t.Item1);
                    int y = getYPositionByPerc(t.Item2) + dy;

                    int table_id = t.Item3;
                    if (Math.Sqrt((x - actualX) * (x - actualX) + (y - actualY) * (y - actualY)) <= ImgScale/2)
                    {
                        if(st.GetServiceRepo().FindTable(table_id).GetStatus() == false)
                        {
                            contextMenuStrip.Items[0].Enabled = true;
                            contextMenuStrip.Items[1].Enabled = false;
                            contextMenuStrip.Items[2].Enabled = false;
                            contextMenuStrip.Items[3].Enabled = false;
                        }
                        else
                        {
                            contextMenuStrip.Items[0].Enabled = false;
                            contextMenuStrip.Items[1].Enabled = true;
                            contextMenuStrip.Items[2].Enabled = true;
                            contextMenuStrip.Items[3].Enabled = true;
                        }

                        current_table = table_id;
                        contextMenuStrip.Show(this, new Point(e.X, e.Y));

                        break;
                    }
                }

            }
        }

        private void ocupyTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            so.AddOrder(current_table, new List<Tuple<int, int>>());
            Refresh();
        }

        private void emptyTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (so.GetRepoOrder().FindOrderByTableId(current_table).GetStatus() == "IN PROGRESS") //to change
            {
                MessageBox.Show("Cannot empty the table while an order is still in progress!", "Warning"); 
                return;
            }
            so.RemoveOrder(so.GetRepoOrder().FindOrderByTableId(current_table).GetId());
            Refresh();
        }

        private void viewOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string order = "Items from this order: \n\n\n\n";
            int price = 0;
            foreach(var o in so.GetRepoOrder().FindOrderByTableId(current_table).GetCommand())
            {
                int crt_price = sp.GetServiceRepo().FindProduct(o.Item1).GetPrice();
                price += crt_price * o.Item2;
                order += sp.GetServiceRepo().FindProduct(o.Item1).GetName() + "\t\tx " + o.Item2.ToString() + "\t\t" + crt_price.ToString() + "$\n";
            }
            order += "\n\n\n\nTotal: " + price.ToString() + "$";

            MessageBox.Show(order, "Order list");
        }

        private void addProductsToOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAddToOrder(so, sp, this, so.GetRepoOrder().FindOrderByTableId(current_table).GetId()).ShowDialog();
        }

        public RestaurantFloor(MainMenu mmenu, ServiceTable st, ServiceRestaurant sr, ServiceOrder so, ServiceProduct sp)
        {
            InitializeComponent(); 
            this.st = st;
            this.sr = sr;
            this.so = so;
            this.sp = sp;
            this.mmenu = mmenu;

            Refresh();
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.SeaGreen), x, y, L, H);


            if (sr.GetRestaurant().GetTables() == null) return;

            Image img4 = Image.FromFile("Assets\\4-seats.png");
            Image img6 = Image.FromFile("Assets\\6-seats.png");
            foreach (var t in sr.GetRestaurant().GetTables())
            {
                int table_id = t.Item3;
                int x = getXPositionByPerc(t.Item1);
                int y = getYPositionByPerc(t.Item2);
                int no_seats = st.GetServiceRepo().FindTable(table_id).GetNoSeats();
                if (no_seats == 4)
                    g.DrawImage(img4, new Rectangle(new Point(x, y + dy), new Size(ImgScale, ImgScale)));
                else if (no_seats == 6) 
                    g.DrawImage(img6, new Rectangle(new Point(x, y + dy), new Size(ImgScale, ImgScale)));

                if (st.GetServiceRepo().FindTable(table_id).GetStatus() == false)
                    g.FillEllipse(new SolidBrush(Color.Green), x, y + dy, 20, 20);
                else
                {
                    g.FillEllipse(new SolidBrush(Color.Red), x, y + dy, 20, 20);

                    if (so.GetRepoOrder().FindOrderByTableId(table_id).GetStatus() == "NOT TAKEN")
                        g.FillEllipse(new SolidBrush(Color.Red), x + 30, y + dy, 20, 20);
                    else if (so.GetRepoOrder().FindOrderByTableId(table_id).GetStatus() == "IN PROGRESS")
                        g.FillEllipse(new SolidBrush(Color.Yellow), x + 30, y + dy, 20, 20);
                    else if (so.GetRepoOrder().FindOrderByTableId(table_id).GetStatus() == "DONE")
                        g.FillEllipse(new SolidBrush(Color.Green), x + 30, y + dy, 20, 20);
                }
            }
        }
        public new void Refresh()
        {
            //panel1.Height = Convert.ToInt32(95.00 / 100.00 * Convert.ToDouble(mmenu.GetPanelSize().Height));
            H = mmenu.GetRestaurantWindowSize().Height;
            L = mmenu.GetRestaurantWindowSize().Width;
            //H = Convert.ToInt32(90.00 / 100.00 * Convert.ToDouble(mmenu.GetPanelSize().Height));
            //L = Convert.ToInt32(Convert.ToDouble(H) * sr.GetRestaurant().GetLength() / sr.GetRestaurant().GetWidth());

            int srl = Convert.ToInt32(sr.GetRestaurant().GetLength());
            int srw = Convert.ToInt32(sr.GetRestaurant().GetWidth());
            ImgScale = Convert.ToInt32(Convert.ToDouble(Math.Max(L, H)) / Convert.ToDouble(Math.Max(srl, srw)));

            x = (mmenu.Width - L) / 2;
            y = (Math.Abs (mmenu.GetPanelSize().Height - H) ) / 2;

            dy = mmenu.GetPanelSize().Height / 10;

            panel1.Invalidate();
        }
    }
}
