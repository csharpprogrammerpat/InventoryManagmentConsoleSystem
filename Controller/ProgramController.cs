using Inventory_Management_System.Manager;
using Inventory_Management_System.Service;
using Inventory_Management_System.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management_System.Controller
{
    // Controller class that manages the interaction between the View and the Manager components
    internal class ProgramController
    {
        private IInventoryManager _inventoryManager; // Interface to handle inventory management logic
        private InventoryManagementSystemView _inventoryManagementSystemView; // View for displaying the user interface

        // Constructor that injects dependencies for InventoryManager and View components
        public ProgramController(IInventoryManager inventoryManager, InventoryManagementSystemView inventoryManagementSystemView)
        {
            _inventoryManager = inventoryManager; 
            _inventoryManagementSystemView = inventoryManagementSystemView; 
        }

        // Method to start the application and display the main menu
        public void Run()
        {
            _inventoryManagementSystemView.DisplayMenu();
        }
    }
}
