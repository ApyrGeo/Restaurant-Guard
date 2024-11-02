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
    public partial class ManageEmployees : Form
    {
        private ServiceEmployee se;
        private MainMenu mmenu;
        public ManageEmployees(ServiceEmployee se, MainMenu mmenu)
        {
            InitializeComponent();
            this.mmenu = mmenu;
            this.se = se;

            dataGridView1.Width = Convert.ToInt32(Convert.ToDouble(mmenu.GetPanelSize().Width) * 3.50 / 5.00);

            Refresh();
        }

        private void ManageEmployees_Load(object sender, EventArgs e)
        {

        }

        private void ManageEmployees_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                se.AddEmployee(txt_salary.Text, txt_name.Text, txt_uname.Text, txt_password.Text);

                txt_name.Text = "";
                txt_password.Text = "";
                txt_uname.Text = "";
                txt_salary.Text = "";
                Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public new void Refresh()
        {
            dataGridView1.Rows.Clear();
            foreach(Employee e in se.GetServiceRepo().GetRepoList())
            {
                dataGridView1.Rows.Add(e.GetId(),e.GetName(),e.GetUsername(), e.GetSalary());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= dataGridView1.Rows.Count - 1) return;

            if(e.ColumnIndex == 4)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to remove this employee?", "Warning",MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    se.DeleteEmployee(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value));
                    Refresh();
                }
            }
        }
    }
}
