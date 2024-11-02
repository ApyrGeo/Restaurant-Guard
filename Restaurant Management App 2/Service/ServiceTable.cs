using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class ServiceTable
    {
        private RepositoryTable repo;
        public ServiceTable(RepositoryTable repo)
        {
            this.repo = repo;
        }

        public ServiceTable AddTable(int seats, bool status = false)
        {
            if (seats < 2) throw new Exception("Too few seats!");

            GetServiceRepo().AddToList(new Table(0, seats, status));

            return this;
        }
        public ServiceTable RemoveTable(int id)
        {
            if (GetServiceRepo().FindTable(id) == null) throw new Exception("Invalid id!");

            GetServiceRepo().RemoveFromList(id);
            return this;
        }
        public ServiceTable ChangeStatusTable(int id, bool status)
        {
            if (GetServiceRepo().FindTable(id) == null) throw new Exception("Invalid id!");

            GetServiceRepo().ChangeStatus(id, status);

            return this;
        }

        public List<Table> GetEmptyTables()
        {
            return GetServiceRepo().GetRepoList().FindAll((Table t) => t.GetStatus() == false);
        }
        public List<Table> GetRangeSeatsTables(int min, int max)
        {
            if (min > max) throw new Exception("Invalid range!");

            return GetServiceRepo().GetRepoList().FindAll((Table t) => min <= t.GetNoSeats() && t.GetNoSeats() <= max);
        }
        public RepositoryTable GetServiceRepo() { return repo; }
    }
}
