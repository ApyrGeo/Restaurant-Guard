using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_App_2.Repository;

namespace Restaurant_Management_App_2
{
    public class ServiceTable
    {
        private RepositoryTable repo;
        public ServiceTable(RepositoryTable repo)
        {
            this.repo = repo;
        }

        public ServiceTable AddTable(int seats, bool status, double X, double Y)
        {
            if (seats < 2) throw new Exception("Too few seats!");

            GetServiceRepo().AddTable(new Table(0, seats, status, X, Y));

            return this;
        }
        public ServiceTable RemoveTable(int id)
        {
            if (GetServiceRepo().GetTable(id) == null) throw new Exception("Invalid id!");

            GetServiceRepo().RemoveTable(id);
            return this;
        }
        public ServiceTable ChangeStatusTable(int id, bool status)
        {
            if (GetServiceRepo().GetTable(id) == null) throw new Exception("Invalid id!");

            GetServiceRepo().ChangeStatus(id, status);

            return this;
        }

        public List<Table> GetEmptyTables()
        {
            return GetServiceRepo().GetTables().FindAll((Table t) => t.GetStatus() == false);
        }
        public List<Table> GetRangeSeatsTables(int min, int max)
        {
            if (min > max) throw new Exception("Invalid range!");

            return GetServiceRepo().GetTables().FindAll((Table t) => min <= t.GetNoSeats() && t.GetNoSeats() <= max);
        }
        public RepositoryTable GetServiceRepo() { return repo; }
    }
}
