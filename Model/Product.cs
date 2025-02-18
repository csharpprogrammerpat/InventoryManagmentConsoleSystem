using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Inventory_Management_System.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }



        public Product(int productId, string name, int quantityInStock, double price)
        {
            ProductId = productId;
            Name = name;
            QuantityInStock = quantityInStock;
            Price = price;
        }

        public Product(string name, int quantityInStock, double price)
        {
            Name = name;
            QuantityInStock = quantityInStock;
            Price = price;
        }

        public Product() { }
    }
}
