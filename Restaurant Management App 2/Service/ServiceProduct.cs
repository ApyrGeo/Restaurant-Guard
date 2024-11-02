using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App
{
    public class ServiceProduct
    {
        private readonly RepositoryProduct repo;
        public ServiceProduct(RepositoryProduct repo)
        {
            this.repo = repo;
        }
        public ServiceProduct AddProduct(string name, string category, string price, string quantity = "")
        {
            if (name.Length < 2) throw new Exception("Invalid name!");
            if (category.Length < 2) throw new Exception("Invalid category!");
            if (quantity.Length != 0 && !quantity.All(char.IsDigit)) throw new Exception("Invalid quantity");
            if (price.Length == 0) throw new Exception("Please enter a price!");
            if (!price.All(char.IsDigit)) throw new Exception("Invalid price!");
            if (Convert.ToInt32(price) < 0) throw new Exception("Price is invalid!");
            if (GetServiceRepo().FindProduct(name) != null) throw new Exception("Product already exists!");

            if (quantity == "")
                GetServiceRepo().AddToList(new Product(0, category, name, Convert.ToInt32(price)));
            else
                GetServiceRepo().AddToList(new ConsumableProduct(0, category, name, Convert.ToInt32(quantity), Convert.ToInt32(price)));

            return this;
        }

        public ServiceProduct DeleteProduct(int id)
        {
            if (GetServiceRepo().FindProduct(id) == null)
                throw new Exception("Invalid id");
            GetServiceRepo().RemoveFromList(id);

            return this;
        }

        public ServiceProduct AddQuantity(int product_id, int quantity)
        {
            if (GetServiceRepo().FindProduct(product_id) == null)
                throw new Exception("Invalid id");

            if (quantity <= 0) throw new Exception("Invalid quantity!");

            GetServiceRepo().AddQuantity(product_id, quantity);

            return this;
        }
        public ServiceProduct RemoveQuantity(int product_id, int quantity)
        {
            if (GetServiceRepo().FindProduct(product_id) == null)
                throw new Exception("Invalid id");

            if (quantity <= 0) throw new Exception("Invalid quantity!");

            if (GetServiceRepo().FindProduct(product_id).GetQuantity() - quantity < 0 && GetServiceRepo().FindProduct(product_id).GetType()==typeof(ConsumableProduct ))
                throw new Exception("Not enough product in stock!");

            GetServiceRepo().AddQuantity(product_id, -quantity);

            return this;
        }
        public RepositoryProduct GetServiceRepo() { return repo; }
    }
}
