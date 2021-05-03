using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Unity;

namespace GR
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var container = new UnityContainer();
            container.RegisterType<IUpdateInventory, UpdateInventory>();

            var inventoryApp = container.Resolve<InventoryApp>();

            var Items = inventoryApp.LoadItems();
            inventoryApp.UpdateInventoryProcess(Items);

            Console.ReadKey();
        }
    }
}
