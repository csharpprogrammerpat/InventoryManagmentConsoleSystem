using Inventory_Management_System.Controller;
using Inventory_Management_System.Helper;
using Inventory_Management_System.Manager;
using Inventory_Management_System.Service;
using Inventory_Management_System.View;
using System;

namespace Inventory_Management_System
{
    public class Program
    {
        static void Main(string[] args)
        {
            IInventoryManager inventoryManager = new InventoryManager(); // create an instance of manager
            InventoryManagementSystemView inventoryManagementSystemView = new InventoryManagementSystemView(inventoryManager); // inject manager to view
            ProgramController programController = new ProgramController(inventoryManagementSystemView); //inject view into controller
            programController.Run(); //run view
        }
    }
}
