using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR
{
    public class InventoryApp : IInventoryApp
    {
        IUpdateInventory _updateInventory;
        public InventoryApp(IUpdateInventory updateInventory)
        {
            this._updateInventory = updateInventory;
        }

        public void UpdateInventoryProcess(List<Item> Items)
        {
            _updateInventory.Update(Items);

            var filename = $"inventory_{DateTime.Now:yyyyddMM-HHmmss}.txt";
            var inventoryOutput = JsonConvert.SerializeObject(Items, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), inventoryOutput);
        }

        public List<Item> GetItems()
        {
            var Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

            return Items;
        }
    }
}
