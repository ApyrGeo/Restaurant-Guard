using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2
{
    public class Order
    {
        private int id, id_table;
        private List<Tuple<int, int>> command = new List<Tuple<int, int>>();
        private string status;

        public Order(int id, int id_table, List<Tuple<int, int>> command, string status)
        {
            this.id = id;
            this.id_table = id_table;
            this.command = command;
            this.status = status;
        }
        public int GetId() { return id; }
        public int GetIdTable() { return id_table; }
        public List<Tuple<int, int>> GetCommand() { return command; }
        public void AddToCommand(Tuple<int, int> item)
        {
            this.command.Add(item);
        }
        public string GetStatus() { return status; }
        public void ChangeStatus(string status) { this.status = status; }
    }
}
