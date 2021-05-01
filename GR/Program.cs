using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace GR
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var updateInventory = new UpdateInventory();

            InventoryApp inventoryApp = new InventoryApp(updateInventory);

            var Items = inventoryApp.GetItems();
            inventoryApp.UpdateInventoryProcess(Items);

            Console.WriteLine("Inventory update complete");
            Console.ReadKey();
        }
    }
}
