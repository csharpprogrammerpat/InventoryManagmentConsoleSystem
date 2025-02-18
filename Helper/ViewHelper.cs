using Inventory_Management_System.Manager;
using Inventory_Management_System.Model;
using Inventory_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace Inventory_Management_System.Helper
{
    internal class ViewHelper
    {
        private readonly IInventoryManager _inventoryManager;
        private string _action { get; set; }
        public ViewHelper(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
            _action = "";
        }
        public void AddProduct()
        {
            _action = "Add product";
            bool addAnother = true;

            while (addAnother)
            {
                Product product = GetProductDetails();
                if (product == null)
                {
                    Console.WriteLine("Failed to add product.");
                    return; // Stop the process and return to the main menu
                }
                Console.WriteLine("");
                _inventoryManager.AddProduct(product);
                addAnother = AskToContinue(_action);  // Ask user if they want to add another product
            }
        }

        public void UpdateProduct()
        {
            _action = "Update product";
            bool updateAnother = true;
            while (updateAnother)
            {
                _inventoryManager.ListProducts();
                Console.WriteLine("Updating a product");
                Console.WriteLine("");
                int productId = GetProductId(); // Get the product ID from the user
                if (productId < 0)
                {
                    Console.WriteLine("invalid input");
                    return;
                }
                Console.WriteLine("Enter new quantity: ");
                int newQuantity = GetQuantity();
                if (newQuantity < 0)
                {
                    Console.WriteLine("invalid input");
                    return;
                }
               bool result = ContinueUpdate(productId, newQuantity);
              // Confirm if the user wants to proceed with update
                if (result)
                {
                    _inventoryManager.UpdateProduct(productId, newQuantity); // Perform the update
                    Console.WriteLine("Product updated successfully.");
                    updateAnother = AskToContinue(_action);  // Ask if the user wants to update another product
                }
                else
                {
                    Console.WriteLine("Product update canceled.");
                    updateAnother = AskToContinue(_action);  // Ask again if the user wants to perform another operation
                }

            }
        }

        public void DisplayTotalInventoryValue()
        {
            Console.WriteLine("");
            _action = "Display Total Value";
            bool viewAgain = true;

            while (viewAgain)
            {
                Console.WriteLine("");
                _inventoryManager.GetTotalValue(); // Display the list of products
                viewAgain = AskToContinue(_action); // Ask user if they want to view again
            }
        }
        private Product GetProductDetails()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product name (type 'cancel' to exit): ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) || name.ToLower() == "cancel")
            {
                Console.WriteLine("Product entry canceled.");
                return null; // Return null to indicate cancellation
            }

            double price = GetPrice();
            if (price < 0)
            {
                return null; // If price is invalid, cancel the operation
            }

            int quantity = GetQuantity();
            if (quantity < 0)
            {
                return null; // If quantity is invalid, cancel the operation
            }

            return new Product(name, quantity, price);
        }

        private int GetProductId()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product Id (or type 'cancel' to exit): ");
            string productId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(productId) || productId.ToLower() == "cancel")
            {
                Console.WriteLine("Product entry canceled.");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }

            if (!int.TryParse(productId, out int quantity))
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return -1; // Return -1 to indicate invalid input
            }

            return quantity;
        }


        private double GetPrice()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product price (or type 'cancel' to exit): ");
            string priceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(priceInput) || priceInput.ToLower() == "cancel")
            {
                Console.WriteLine("Product entry canceled.");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }

            if (!double.TryParse(priceInput, out double price))
            {
                Console.WriteLine("Invalid price. Operation canceled.");
                return -1; // Return -1 to indicate invalid input
            }

            return price;
        }

        private int GetQuantity()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product quantity (or type 'cancel' to exit): ");
            string quantityInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(quantityInput) || quantityInput.ToLower() == "cancel")
            {
                Console.WriteLine("Product entry canceled.");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }

            if (!int.TryParse(quantityInput, out int quantity))
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return -1; // Return -1 to indicate invalid input
            }

            return quantity;
        }



        public void RemoveProduct()
        {
            Console.WriteLine("");
            _action = "Remove product";
            bool removeAnother = true;

            while (removeAnother)
            {
                _inventoryManager.ListProducts();
                Console.WriteLine("Removing a Product");
                int productId = GetProductId();
                if (productId < 0)
                {
                    Console.WriteLine("invalid input");
                    return;
                }
                bool result = ContinueDeletion(productId);
                if (result)
                {
                    Console.WriteLine("");
                    _inventoryManager.RemoveProduct(productId);
                    removeAnother = AskToContinue(_action); // Ask user if they want to remove another product
                }
                else
                {
                    Console.WriteLine("Product deletion canceled.");
                    removeAnother = AskToContinue(_action);  // Ask again if the user wants to perform another operation
                }
            }
        }

        public void DisplayProducts()
        {
            Console.WriteLine("");
            _action = "View products";
            bool viewAgain = true;

            while (viewAgain)
            {
                _inventoryManager.ListProducts(); // Display the list of products
                viewAgain = AskToContinue(_action); // Ask user if they want to view again
            }
        }
        public bool AskToContinue(string action)
        {
            Console.WriteLine("");
            Console.WriteLine($"Do you want to perform \"{action}\" again? Y | N");
            string userInput = Console.ReadLine()?.Trim().ToUpper(); // Read and trim input

            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }

            return userInput == "Y";
        }

        public bool ContinueDeletion(int productId)
        {
            Product product = _inventoryManager.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Invalid Id, Deletion Failed.");
                Console.WriteLine("");
                return false;
            }
            Console.WriteLine($"Are you sure you want to delete the product {product.Name} with ID {product.ProductId} Y | N ?");
            string userInput = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }
            return userInput == "Y";
        }

        public bool ContinueUpdate(int productId, int newQuantity)
        {
            Product product = _inventoryManager.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine("Invalid Id, Update Failed");
                Console.WriteLine("");
                return false;
            }
            Console.WriteLine($"Are you sure you want to update the product {product.Name} with ID {product.ProductId} with new Quantity {newQuantity} Y | N ?");
            string userInput = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }
            return userInput == "Y";
        }
    }
}
