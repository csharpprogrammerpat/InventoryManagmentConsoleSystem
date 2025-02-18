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
            IInventoryManager inventoryManager = new InventoryManager();
            InventoryManagementSystemView inventoryManagementSystemView = new InventoryManagementSystemView(inventoryManager);
            ProgramController programController = new ProgramController(inventoryManager, inventoryManagementSystemView);
            programController.Run();
        }
    }
}
