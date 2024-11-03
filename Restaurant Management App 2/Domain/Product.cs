using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_App_2
{
    public class Product
    {
        protected int id, id_rest, quantity=0, price;
        protected string name, category;

        public Product(int id, string category, string name, int price)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.price = price;
        }
        public int GetId() { return id; }
        public string GetName() { return name; }
        public string GetCategory() { return category; }
        public int GetPrice() { return price; }
        virtual public int GetQuantity() { return 0; }
        virtual public void IncreaseQuantity(int quantity) { }
    }
    class ConsumableProduct : Product
    {
        public ConsumableProduct(int id, string category, string name, int quantity, int price): base(id,name,category, price)
        {
            this.quantity = quantity;
        }
        public override int GetQuantity() { return quantity; }
        public override void IncreaseQuantity(int quantity) { this.quantity += quantity;}
    }

}
