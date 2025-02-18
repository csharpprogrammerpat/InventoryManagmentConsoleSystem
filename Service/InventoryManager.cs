using Inventory_Management_System.Model;
using Inventory_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Manager
{
    public class InventoryManager : IInventoryManager
    {
        private List<Product> inventory;
        private int nextProductId = 0;

        public InventoryManager()
        {
            inventory = new List<Product>();
        }


        public void AddProduct(Product product)
        {


            try
            {
                string error = ValueChecker(product.Price, product.QuantityInStock, product.Name);
                if (error != null)
                {
                    Console.WriteLine(error);
                    return;
                }

                nextProductId = nextProductId + 1;
                product.ProductId = nextProductId;
                inventory.Add(product);
                Console.WriteLine($"Product '{product.Name}' (ID: {product.ProductId} Price: {product.Price} Quantity: {product.QuantityInStock}) added successfully.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }
        }

        public Product GetProductById(int productId)
        {
           
            foreach (var product in inventory)
            {
               
                if (product.ProductId == productId)
                {
                    return product;
                }
            }
            return null;
        }

        public double GetTotalValue()
        {
            double totalValue = 0;
            foreach (var product in inventory)
            {
                totalValue = product.Price * product.QuantityInStock;
            }
            Console.WriteLine($"Total inventory value: Php {totalValue:F2}");
            return totalValue;
        }

      
        public void ListProducts()
        {
            if (!inventory.Any())
            {
                Console.WriteLine("");
                Console.WriteLine("Inventory is empty.");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("=======================================================");
            Console.WriteLine("                   Current Inventory                    ");
            Console.WriteLine("=======================================================");
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("{0,-5} {1,-50} {2,-10} {3,-30}", "ID", "Name", "Quantity", "Price");
            Console.WriteLine("------------------------------------------------------------------------------");

            foreach (var product in inventory)
            {
                Console.WriteLine("{0,-5} {1,-50} {2,-10} {3,-30}",
                    product.ProductId,
                    product.Name.Length > 50 ? product.Name[..47] + "..." : product.Name,
                    product.QuantityInStock,
                    $"Php {product.Price:F2}");
            }
        }
        public void RemoveProduct(int productId)
        {
            Product productToRemove = null;
            foreach (var product in inventory)
            {
                if (product.ProductId == productId)
                {
                    productToRemove = product;
                    break;
                }
            }
            if (productToRemove == null)
            {
                Console.WriteLine($"Product with ID {productId} not found.");
                return;
            }

            inventory.Remove(productToRemove);

            // Print success message
            Console.WriteLine($"Product '{productToRemove.Name}' with ID {productId} removed successfully.");
            return;
        }

        public void UpdateProduct(int productId, int newQuantity)
        {
            Product productToEdit = null;
            foreach (var product in inventory)
            {
                if (product.ProductId == productId)
                {
                    productToEdit = product;
                    break;
                }
            }
            if (productToEdit == null)
            {
                Console.WriteLine($"Product with ID {productId} not found.");
                return;
            }

           
            productToEdit.QuantityInStock = newQuantity;
            Console.WriteLine($"Product '{productToEdit.Name}' with ID: {productId} updated to new Quantity: {newQuantity}.");
            return;
        }

        private string ValueChecker(double price, int quantity, string name)
        {
            List<string> errors = new List<string>();

            if (price < 0)
                errors.Add("Error: Price cannot be negative.");
            if (quantity < 0)
                errors.Add("Error: Quantity cannot be negative.");
            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Error: Name cannot be Null or Blank.");


            return errors.Count > 0 ? string.Join("\n", errors) : null;
        }



    }
}
