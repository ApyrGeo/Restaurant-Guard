using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Org.BouncyCastle.Ocsp;
using Google.Protobuf.WellKnownTypes;

namespace Restaurant_Management_App_2
{
    public class Restaurant
    {
        private int id, manager_id;
        private double L, W;
        private string name, password;

        public Restaurant(int id, int id_manager, double L, double W, string name, string password)
        {
            this.id = id;
            this.manager_id = id_manager;
            this.L = L;
            this.W = W;
            this.name = name;
            this.password = password;
        }
        
        public int GetId() { return id; }
        public double GetLength() { return L; }
        public double GetWidth() {  return W; }
        public int GetManagerId() {  return manager_id; }
        public string GetName() { return name; }
        public string GetPassword() { return password; }
    }
}
