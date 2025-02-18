using Inventory_Management_System.Manager;
using Inventory_Management_System.Model;
using Inventory_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
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
                    Console.WriteLine("Failed to add product. Invalid Input Or Cancelled");
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
                Console.WriteLine("Updating a product.");
                Console.WriteLine("");
                int productId = GetProductId(); // Get the product ID from the user
                if (productId < 0)
                {
                    Console.WriteLine("invalid input or cancelled.");
                    return;
                }
                Console.WriteLine("Enter new quantity: ");
                int newQuantity = GetQuantity();
                if (newQuantity < 0)
                {
                    Console.WriteLine("invalid input or cancelled.");
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

        public void DisplayTotalInventoryValue() // displays total inventory value
        {
            Console.WriteLine("");
            _action = "Display Total Value";
            bool viewAgain = true;

            while (viewAgain)
            {
                Console.WriteLine("");
                _inventoryManager.GetTotalValue(); 
                viewAgain = AskToContinue(_action); // Ask user if they want to view again
            }
        }
        private Product GetProductDetails()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product name (type 'cancel' to exit): ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name)) //checks if the inputed value is null or whitespace only or the word cancel
            {
                Console.WriteLine("invalid name. name is null.");
                return null; // Return null to indicate cancellation
            }
            if (name.ToLower() == "cancel") // Converts name to lowercase and checks if it's "cancel"
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

        private int GetProductId() //a helper to get product id value and check if correct value
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product Id (or type 'cancel' to exit): ");
            string productId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(productId)) //checks if the inputed value is null or whitespace only or the word cancel
            {
                Console.WriteLine("Invalid input. product id is null or blank");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }
            if (productId.ToLower() == "cancel") // Converts name to lowercase and checks if it's "cancel"
            {
                Console.WriteLine("Product entry canceled.");
                return -1; // Return null to indicate cancellation
            }

            if (!int.TryParse(productId, out int id)) //cast product ID to int if false returns -1
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return -1; // Return -1 to indicate invalid input
            }

            return id;
        }


        private double GetPrice()//a helper to get price value and check if correct value
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product price (or type 'cancel' to exit): ");
            string priceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(priceInput)) //checks if the inputed value is null or whitespace only or the word cancel
            {
                Console.WriteLine("Invalid Input. price input is null or blank");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }
            if (priceInput.ToLower() == "cancel") // Converts name to lowercase and checks if it's "cancel"
            {
                Console.WriteLine("Product entry canceled.");
                return -1; // Return null to indicate cancellation
            }

            if (!double.TryParse(priceInput, out double price)) //cast priceInput to double if false returns -1
            {
                Console.WriteLine("Invalid price. Operation canceled.");
                return -1; // Return -1 to indicate invalid input
            }

            return price;
        }

        private int GetQuantity() //a helper to get quantity value and check if correct value
        {
            Console.WriteLine("");
            Console.WriteLine("Enter product quantity (or type 'cancel' to exit): ");
            string quantityInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(quantityInput)) //checks if the inputed value is null or whitespace only or the word cancel
            {
                Console.WriteLine("Invalid Quantity, quanitity is null or blank.");
                return -1; // Return an invalid value (-1) to indicate cancellation or error
            }
            if (quantityInput.ToLower() == "cancel")
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



        public void RemoveProduct() //deletes a product
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
                    Console.WriteLine("invalid input or cancelled.");
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

        public void DisplayProducts() // Display the list of products
        {
            Console.WriteLine("");
            _action = "View products";
            bool viewAgain = true;

            while (viewAgain)
            {
                _inventoryManager.ListProducts(); 
                viewAgain = AskToContinue(_action); // Ask user if they want to do the action again
            }
        }
        public bool AskToContinue(string action)//method where it asks user to perform the current action
        {
            Console.WriteLine("");
            Console.WriteLine($"Do you want to perform \"{action}\" again? Y | N"); //asking user if they want to perform the current action.
            string userInput = Console.ReadLine()?.Trim().ToUpper(); // Read and trim input

            if (string.IsNullOrEmpty(userInput))
            {
                return false;
            }

            return userInput == "Y"; //return if userInput is Y then it returns true.
        }

        public bool ContinueDeletion(int productId) //a method to check if user is sure to delete the product.
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
            return userInput == "Y"; //return if userInput is Y then it returns true.
        }

        public bool ContinueUpdate(int productId, int newQuantity)//a method to check if user is sure to update the product.
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
            return userInput == "Y"; //return if userInput is Y then it returns true.
        }
    }
}
