using Inventory_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management_System.Service
{
    public interface IInventoryManager
    {
        public void AddProduct(Product product);
        public double GetTotalValue();
        public void ListProducts();
        public Product GetProductById(int productId);
        public void RemoveProduct(int productId);
        public void UpdateProduct(int productId, int newQuantity);
    }
}
