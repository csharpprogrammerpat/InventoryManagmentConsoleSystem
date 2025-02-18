using Inventory_Management_System.Manager;
using Inventory_Management_System.Service;
using Inventory_Management_System.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Management_System.Controller
{
    internal class ProgramController
    {

        private IInventoryManager _inventoryManager;
        private InventoryManagementSystemView _inventoryManagementSystemView;
        

        // Constructor receives the interfaces as dependencies
        public ProgramController(IInventoryManager inventoryManager, InventoryManagementSystemView inventoryManagementSystemView)
        {
            _inventoryManager = inventoryManager;
            _inventoryManagementSystemView = inventoryManagementSystemView;
        }

        public void Run()
        {
            // Start the interaction by displaying the menu
            _inventoryManagementSystemView.DisplayMenu();
        }
    }
}
