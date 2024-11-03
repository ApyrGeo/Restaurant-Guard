using Org.BouncyCastle.Tls;
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
    public partial class ManageRestaurant : Form
    {
        private MainMenu mmenu;
        private ServiceExistingRestaurant sr;
        private ServiceTable st;
        private int L, H, x, y;
        private int ImgScale;
        private new bool CanSelect = false;
        private bool CanDelete = false;

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
        public ManageRestaurant(MainMenu mmenu, ServiceExistingRestaurant sr, ServiceTable st)
        {
            InitializeComponent();
            this.mmenu = mmenu;
            this.sr = sr;
            this.st = st;

            Refresh();
        }
        private bool CanBringModifications()
        {
            if(st.GetEmptyTables().Count() != st.GetServiceRepo().GetRepoList().Count())
                return false;
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!CanBringModifications())
            { MessageBox.Show("You cannot bring modifications while tables are occupied!", "Error!"); return; }

            if (comboBox1.SelectedIndex == -1) { MessageBox.Show("Please select the number of seats!"); return; }

            label2.Visible = !label2.Visible;
            CanSelect = !CanSelect;

            CanDelete = false;
            label3.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!CanSelect && !CanDelete) return;
            if (e.Location.X < x || e.Location.X > L + x) {MessageBox.Show("Please click on the restaurant floor!"); return; }
            if (e.Location.Y < y || e.Location.Y > H + y) {MessageBox.Show("Please click on the restaurant floor!"); return;}

            if(CanSelect)
            {
                int seats = 0;
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        { seats = 4; break; }
                    case 1:
                        { seats = 6; break; }
                    default: break;
                }
                try
                {
                    //MessageBox.Show(getPercentageWidth(e.Location.X).ToString() + " " + getPercentagehHeight(e.Location.Y).ToString());
                    sr.AddTable(getPercentageWidth(e.Location.X - ImgScale / 2), getPercentagehHeight(e.Location.Y - ImgScale / 2), seats);

                }
                catch(Exception ex) { MessageBox.Show(ex.Message, "Error"); return; }

                //CanSelect = false;
                //label2.Visible = false;

                Refresh();

                //MessageBox.Show("Table added successfully!");
            }
            else if(CanDelete)
            {
                CanDelete = false;
                double actualX = e.Location.X - ImgScale / 2;
                double actualY = e.Location.Y - ImgScale / 2;

                foreach (var t in sr.GetRestaurant().GetTables())
                {
                    int x = getXPositionByPerc(t.Item1);
                    int y = getYPositionByPerc(t.Item2);

                    if (Math.Sqrt((x - actualX) * (x - actualX) + (y - actualY) * (y - actualY)) <= ImgScale /2)
                    { sr.RemoveTable(t.Item3); break; }
                }

                //CanDelete = false;
                //label3.Visible = false;

                Refresh();
                //MessageBox.Show("Table removeds!");
                CanDelete = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!CanBringModifications())
            { MessageBox.Show("You cannot bring modifications while tables are occupied!", "Error!"); return; }    

            CanSelect = false;
            label2.Visible = false;

            CanDelete = !CanDelete;
            label3.Visible = !label3.Visible;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.SeaGreen), x, y, L, H);

            if (CanBringModifications())
            {
                g.FillEllipse(new SolidBrush(Color.DarkGreen), 0, 0, 100, 100);
                g.DrawString("Can\nmodify",new Font(FontFamily.GenericSansSerif, 16), new SolidBrush(Color.Black), new PointF(15,15));
            }
            else
            {
                g.FillEllipse(new SolidBrush(Color.Red), 0, 0, 100, 100);
                g.DrawString("Cannot\nmodify", new Font(FontFamily.GenericSansSerif, 16), new SolidBrush(Color.Black), new PointF(15, 15));

            }

            if (sr.GetRestaurant().GetTables() == null) return;

            Image img4 = Image.FromFile("Assets\\4-seats.png");
            Image img6 = Image.FromFile("Assets\\6-seats.png");
            foreach (var t in sr.GetRestaurant().GetTables())
            {
                int x = getXPositionByPerc(t.Item1);
                int y = getYPositionByPerc(t.Item2);

                int table_id = t.Item3;

                try
                {
                    if (st.GetServiceRepo().FindTable(table_id) == null) continue;
                    int no_seats = st.GetServiceRepo().FindTable(table_id).GetNoSeats();
                    if (no_seats == 4)
                        g.DrawImage(img4, new Rectangle(new Point(x, y), new Size(ImgScale, ImgScale)));
                    else if (no_seats == 6)
                        g.DrawImage(img6, new Rectangle(new Point(x, y), new Size(ImgScale, ImgScale)));
                }
                catch (ArgumentNullException ) { }
            }
        }
        public new void Refresh()
        {
            panel1.Height = Convert.ToInt32(80.00 / 100.00 * Convert.ToDouble(mmenu.GetPanelSize().Height));

            H = Convert.ToInt32(80.00 / 100.00 * Convert.ToDouble(mmenu.GetPanelSize().Height));
            L = Convert.ToInt32(Convert.ToDouble(H) * sr.GetRestaurant().GetLength() / sr.GetRestaurant().GetWidth());

            int srl = Convert.ToInt32(sr.GetRestaurant().GetLength());
            int srw = Convert.ToInt32(sr.GetRestaurant().GetWidth());
            ImgScale = Convert.ToInt32(Convert.ToDouble(Math.Max(L, H)) / Convert.ToDouble(Math.Max(srl, srw)));
            x = (mmenu.Width - L) / 2;
            y = (panel1.Height - H) / 2;

            panel1.Invalidate();
        }
        public Size GetDimensions()
        {
            return new Size(L, H);
        }
        private void ManageRestaurant_Load(object sender, EventArgs e)
        {

        }
    }
}
