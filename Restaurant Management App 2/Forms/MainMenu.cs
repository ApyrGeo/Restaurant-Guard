using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Restaurant_Management_App_2;
using Restaurant_Management_App_2.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Restaurant_Management_App_2
{
    public partial class MainMenu : Form
    {
        private Timer changeLogTimer;
        private DateTime lastCheckedTime;

        private Timer timerLogOut;

        private MySqlConnection con;

        private readonly ServiceTable ServiceTable;
        private readonly ServiceEmployee ServiceEmployee;
        private readonly ServiceOrder ServiceOrder;
        private readonly ServiceProduct ServiceProduct;
        private readonly ServiceExistingRestaurant ServiceRestaurant;
        private Kitchen kitchen_window;
        private ManageProducts products_window;
        private ManageEmployees employees_window;
        private ManageRestaurant restaurant_window;
        private RestaurantFloor floor_window;

        public MainMenu(Restaurant restaurant)
        {
            InitializeComponent();

            this.MinimumSize = new Size(Screen.FromControl(this).Bounds.Width,Screen.FromControl(this).Bounds.Height);

            ServiceEmployee = new ServiceEmployee(new RepositoryEmployee(restaurant.GetId()));
            ServiceTable = new ServiceTable(new RepositoryTable(restaurant.GetId()));
            ServiceProduct = new ServiceProduct(new RepositoryProduct(restaurant.GetId()));
            ServiceOrder = new ServiceOrder(new RepositoryOrder(restaurant.GetId()), ServiceTable.GetServiceRepo(), ServiceProduct.GetServiceRepo());
            ServiceRestaurant = new ServiceExistingRestaurant(new RepositoryExistingRestaurant(restaurant.GetId()));

            kitchen_window = new Kitchen(ServiceOrder, ServiceProduct);
            kitchen_window.TopLevel = false;
            products_window = new ManageProducts(ServiceProduct, this);
            products_window.TopLevel = false;
            employees_window = new ManageEmployees(ServiceEmployee, this);
            employees_window.TopLevel = false;
            restaurant_window = new ManageRestaurant(this, ServiceRestaurant, ServiceTable);
            restaurant_window.TopLevel = false;
            floor_window = new RestaurantFloor(this, ServiceTable, ServiceRestaurant, ServiceOrder, ServiceProduct);
            floor_window.TopLevel = false;

            con = Connection.GetInstance().GetCon();

            InitializeTimer();
            lastCheckedTime = DateTime.Now;

            InitializeTimerLogOut();

            LockKitchen();
            LockAdmin();
            LockEmployee();

            this.Text = "Restaurant Guard - " + restaurant.GetName();
        }
        //TODO refactor this code part in a separate class
        private void InitializeTimerLogOut()
        {
            timerLogOut = new Timer();
            timerLogOut.Interval = 120000;
            timerLogOut.Tick += new EventHandler(Lock);
            timerLogOut.Start();
        }
        private void Lock(object sender, EventArgs e)
        {
            LockKitchen();
            LockAdmin();
        }
        private void InitializeTimer()
        {
            changeLogTimer = new Timer();
            changeLogTimer.Interval = 5 * 1000;
            changeLogTimer.Tick += new EventHandler(CheckForChanges);
            changeLogTimer.Start();
        }
        private void CheckForChanges(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM ChangeLog WHERE timestamp > @last AND id_rest={ServiceRestaurant.GetRestaurant().GetId()}", con);
            cmd.Parameters.AddWithValue("@last", lastCheckedTime.ToString("yyyy-MM-dd HH:mm:ss"));

            lastCheckedTime = DateTime.Now;
            MySqlDataReader read = cmd.ExecuteReader();

            List<string> updates = new List<string>();
            bool requiresUpdate = false;

            while (read.Read())
            {
                requiresUpdate = true;
                updates.Add(read.GetString(2));
            }
            read.Close();

            if (!requiresUpdate) return;

            updates.ForEach(table_name => { 
                if (table_name == "Products")
                {
                    products_window.Refresh();
                }
                else if (table_name == "Employees")
                {
                    employees_window.Refresh();
                }
                else if (table_name == "Tables")
                {
                    employees_window.Refresh();
                    restaurant_window.Refresh();
                    floor_window.Refresh();
                }
                else if (table_name == "Orders")
                {
                    employees_window.Refresh();
                    kitchen_window.Refresh();
                    floor_window.Refresh();
                }
                else if (table_name == "Restaurants")
                {
                    restaurant_window.Refresh();
                }
            });
            
        }
        //
        private void accessKitchenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(kitchen_window);
            kitchen_window.WindowState = FormWindowState.Maximized;
            kitchen_window.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
        public void UnlockKitchen()
        {
            accessKitchenToolStripMenuItem.Enabled = true;
            accessKitchenToolStripMenuItem.Visible = true;
            logInToolStripMenuItem1.Visible = false;
            logInToolStripMenuItem1.Enabled = false;
        }
        public void LockKitchen()
        {
            accessKitchenToolStripMenuItem.Enabled = false;
            accessKitchenToolStripMenuItem.Visible = false;
            logInToolStripMenuItem1.Visible = true;
            logInToolStripMenuItem1.Enabled = true;
        }
        public void LockEmployee()
        {
            tablesToolStripMenuItem.Enabled = false;
            tablesToolStripMenuItem.Visible = false;
            logInToolStripMenuItem.Visible = true;
            logInToolStripMenuItem.Enabled = true;
        }
        public void UnlockEmployee()
        {
            tablesToolStripMenuItem.Enabled = true; 
            tablesToolStripMenuItem.Visible = true;
            logInToolStripMenuItem.Visible = false;
            logInToolStripMenuItem.Enabled = false;
        }
        public void UnlockAdmin()
        {
            logInToolStripMenuItem2.Enabled = false;
            logInToolStripMenuItem2.Visible = false;
            manageEmployeesToolStripMenuItem.Enabled = true;
            manageEmployeesToolStripMenuItem.Visible = true;
            manageProductsToolStripMenuItem.Enabled = true;
            manageProductsToolStripMenuItem.Visible = true;
            manageRestaurantToolStripMenuItem.Enabled = true;
            manageRestaurantToolStripMenuItem.Visible = true;
        }
        public void LockAdmin()
        {
            logInToolStripMenuItem2.Enabled = true;
            logInToolStripMenuItem2.Visible = true;
            manageEmployeesToolStripMenuItem.Enabled = false;
            manageEmployeesToolStripMenuItem.Visible = false;
            manageProductsToolStripMenuItem.Enabled = false;
            manageProductsToolStripMenuItem.Visible = false;
            manageRestaurantToolStripMenuItem.Enabled = false;
            manageRestaurantToolStripMenuItem.Visible = false;
        }

        private void logInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new EmployeeLogIn(ServiceEmployee, this).ShowDialog();
        }

        private void manageProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(products_window);
            products_window.WindowState = FormWindowState.Maximized;
            products_window.Show();
        }

        private void logInToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new ManagerLogIn(this).ShowDialog();
        }

        private void manageEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(employees_window);
            employees_window.WindowState = FormWindowState.Maximized;
            employees_window.Show();
        }

        private void MainMenu_ResizeEnd(object sender, EventArgs e)
        {
        }
        public Size GetPanelSize()
        {
            return panel1.Size;
        }

        private void manageRestaurantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(restaurant_window);
            restaurant_window.WindowState = FormWindowState.Maximized;
            restaurant_window.Show();
        }

        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(floor_window);
            floor_window.WindowState = FormWindowState.Maximized;
            floor_window.Show();
        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeLogIn(ServiceEmployee, this).ShowDialog();
        }

        public Size GetRestaurantWindowSize()
        {
            return restaurant_window.GetDimensions();
        }
    }
}
