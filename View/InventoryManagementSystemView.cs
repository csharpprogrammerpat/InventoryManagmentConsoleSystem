using Inventory_Management_System.Helper;
using Inventory_Management_System.Manager;
using Inventory_Management_System.Model;
using Inventory_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Inventory_Management_System.View
{
    public class InventoryManagementSystemView
    {
        private readonly IInventoryManager _inventoryManager;
        private readonly ViewHelper _viewHelper; //a class that has the helperview


        public InventoryManagementSystemView(IInventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
            _viewHelper = new ViewHelper(inventoryManager); // Initialize the helper
        }

        // Displaying the menu to the user
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("    Inventory Management System          ");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. List Products");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Get Total Value");
                Console.WriteLine("6. Exit");
                Console.WriteLine("==========================================");
                Console.WriteLine("");
                Console.Write("Please select an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _viewHelper.AddProduct();

                        break;
                    case "2":
                        _viewHelper.DisplayProducts();

                        break;
                    case "3":
                        _viewHelper.UpdateProduct();

                        break;
                    case "4":
                        _viewHelper.RemoveProduct();

                        break;
                    case "5":
                        _viewHelper.DisplayTotalInventoryValue();
                        break;
                    case "6":
                        Console.WriteLine("Exiting the Application");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }

    }
}


