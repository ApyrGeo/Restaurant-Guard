using MySql.Data.MySqlClient;
using Restaurant_Management_App;
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

namespace Restaurant_Management_App
{
    public partial class MainMenu : Form
    {
        private Timer changeLogTimer;
        private DateTime lastCheckedTime;

        private Timer timerLogOut;

        private Connection con;

        private readonly ServiceTable ServiceTable;
        private readonly ServiceEmployee ServiceEmployee;
        private readonly ServiceOrder ServiceOrder;
        private readonly ServiceProduct ServiceProduct;
        private readonly ServiceRestaurant ServiceRestaurant;
        private Kitchen kitchen_window;
        private ManageProducts products_window;
        private ManageEmployees employees_window;
        private ManageRestaurant restaurant_window;
        private RestaurantFloor floor_window;

        public MainMenu(Restaurant restaurant)
        {
            InitializeComponent();

            this.MinimumSize = new Size(Screen.FromControl(this).Bounds.Width,Screen.FromControl(this).Bounds.Height);

            ServiceEmployee = new ServiceEmployee(new RepositoryFileEmployee(restaurant.GetId()));
            ServiceTable = new ServiceTable(new RepositoryFileTable(restaurant.GetId()));
            ServiceProduct = new ServiceProduct(new RepositoryFileProduct(restaurant.GetId()));
            ServiceOrder = new ServiceOrder(new RepositoryFileOrder(restaurant.GetId()), ServiceTable.GetServiceRepo(), ServiceProduct.GetServiceRepo());
            ServiceRestaurant = new ServiceRestaurant(restaurant, ServiceTable.GetServiceRepo());

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

            con = new Connection();

            InitializeTimer();
            lastCheckedTime = DateTime.Now;

            InitializeTimerLogOut();

            LockKitchen();
            LockAdmin();
            LockEmployee();

            this.Text = "Restaurant Guard - " + restaurant.GetName();
        }
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
            con.Open();

            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM ChangeLog WHERE timestamp > @last AND id_rest={ServiceRestaurant.GetRestaurant().GetId()}", con.GetCon());
            cmd.Parameters.AddWithValue("@last", lastCheckedTime.ToString("yyyy-MM-dd HH:mm:ss"));
            MySqlDataReader read = cmd.ExecuteReader();

            while (read.Read())
            {
                string table_name = read.GetString(2);

                if (table_name == "Products")
                {
                    ServiceProduct.GetServiceRepo().Refresh();
                    products_window.Refresh();
                }
                else if (table_name == "Employees")
                {
                    ServiceEmployee.GetServiceRepo().Refresh();
                    employees_window.Refresh();
                }
                else if (table_name == "Tables")
                {
                    ServiceTable.GetServiceRepo().Refresh();
                    employees_window.Refresh();
                    restaurant_window.Refresh();
                    floor_window.Refresh();
                }
                else if (table_name == "Orders")
                {
                    ServiceOrder.GetRepoOrder().Refresh();
                    employees_window.Refresh();
                    kitchen_window.Refresh();
                    floor_window.Refresh();
                }
                else if (table_name == "Restaurants")
                {
                    ServiceRestaurant.GetRestaurant().Refresh();
                    restaurant_window.Refresh();
                }

            }
            lastCheckedTime = DateTime.Now - TimeSpan.FromSeconds(10);
            con.Close();
        }
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
