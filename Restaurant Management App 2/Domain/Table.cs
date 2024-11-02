using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class Table
    {
        private int id, no_seats;
        private bool is_occupied;
        public Table(int id, int no_seats, bool is_occupied = false) 
        {
            this.id = id;
            this.is_occupied = is_occupied;
            this.no_seats = no_seats;
        }
        public int GetId() { return id; }
        public int GetNoSeats() { return no_seats; }
        public bool GetStatus() { return is_occupied; }
        public void ChangeStatus(bool status) {  is_occupied = status; }
    }
}
