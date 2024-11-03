using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant_Management_App_2.Repository;

namespace Restaurant_Management_App_2
{
    public class ServiceOrder
    {
        private readonly RepositoryOrder repoO;
        private readonly RepositoryTable repoT;
        private readonly RepositoryProduct repoP;
        
        public ServiceOrder(RepositoryOrder repoO, RepositoryTable repoT, RepositoryProduct repoP)
        {
            this.repoO = repoO;
            this.repoT = repoT;
            this.repoP = repoP;
        }
        public RepositoryOrder GetRepoOrder() { return repoO; }
        public RepositoryTable GetRepoTable() {  return repoT; }
        public RepositoryProduct GetRepoProduct() {  return repoP; }
        public ServiceOrder AddOrder(int table_id, List<Tuple<int, int>> list, string status="NOT TAKEN")
        {
            if (GetRepoTable().GetTable(table_id) == null) throw new Exception("Table does not exist!");
            if (GetRepoTable().GetTable(table_id).GetStatus() == true) throw new Exception("Table is occupied!");
           

            foreach(var item in list)
            {
                if (GetRepoProduct().GetProduct(item.Item1) == null)
                    throw new Exception("Item does not exist!");

                if (GetRepoProduct().GetProduct(item.Item1).GetQuantity() - item.Item2 < 0)
                    throw new Exception("The given quantity is too large for the current stock!");
            }

            foreach (var item in list)
            {
                GetRepoProduct().AddQuantity(item.Item1, -item.Item2);
            }

            GetRepoOrder().AddOrder(new Order(0, table_id, list, status));

            GetRepoTable().ChangeStatus(table_id, true);

            return this;
        }
        public ServiceOrder RemoveOrder(int order_id)
        {
            GetRepoTable().ChangeStatus(GetRepoOrder().FindOrder(order_id).GetIdTable(), false);

            GetRepoOrder().RemoveOrder(order_id);

            return this;
        }
        public ServiceOrder ChangeStatus(int id, string status)
        {
            if (GetRepoOrder().FindOrder(id) == null)
                throw new Exception("Invalid id!");

            List<string> l = new List<string>() { "NOT TAKEN", "IN PROGRESS", "DONE" };
            bool ok = false;
            foreach (var i in l)
            {
                if (i == status) {ok = true; break; }
            }
            if (!ok)
                throw new Exception("Invalid Status!");

            GetRepoOrder().ChangeStatus(id, status);

            return this;
        }
        public ServiceOrder AddCommandToOrder(int order_id, Tuple<int,int> item)
        {
            if (GetRepoOrder().FindOrder(order_id) == null) throw new Exception("Invalid Order Id!");
            if (GetRepoProduct().GetProduct(item.Item1) == null) throw new Exception("Invalid Product!");
            if (item.Item2 <= 0) throw new Exception("Invalid Count!");

            if (GetRepoProduct().GetProduct(item.Item1).GetQuantity() - item.Item2 < 0 && repoP.GetProduct(item.Item1).GetType() == typeof(ConsumableProduct))
                throw new Exception("The given quantity is too large for the current stock!");

            GetRepoOrder().AddToOrder(order_id, item);

            GetRepoOrder().ChangeStatus(order_id, "IN PROGRESS");

            if(GetRepoOrder().FindOrder(order_id).GetType() == typeof(ConsumableProduct))
                GetRepoProduct().AddQuantity(item.Item1, -item.Item2);

            return this;
        }
        public List<Order> GetOrders() 
        { 
            return GetRepoOrder().GetOrders(); 
        }
        public List<Order> GetOrders(string status) 
        {  
            return GetRepoOrder().GetOrders().FindAll((Order o) => o.GetStatus() == status); 
        }
    }
}
