﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace GR
{
    public class Program
    {
        public IList<Item> Items;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            var app = new Program()
            {
                Items = new List<Item>
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
                }
            };

            app.UpdateInventory();

            var filename = $"inventory_{DateTime.Now:yyyyddMM-HHmmss}.txt";
            var inventoryOutput = JsonConvert.SerializeObject(app.Items, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename), inventoryOutput);

            Console.ReadKey();
        }

        public void UpdateInventory()
        {
            Console.WriteLine("Updating inventory");


            foreach (var item in Items)
            {
                Console.WriteLine(" - Item: {0}", item.Name);

                // we can ignore if item is sulfuras and work on rest of the inventory items
                if (item.Name == "Sulfuras, Hand of Ragnaros")
                    continue;
                else
                {
                    // every item except sulfuras, drops its sellin value everyday.
                    item.SellIn = item.SellIn - 1;

                    // except 'aged brie', 'backstage passes' & 'sulfuras' all other items diminshes in quality
                    if (item.Name != "Aged Brie" && !item.Name.Contains("Backstage passes"))
                    {
                        item.Quality = item.Quality > 0 ? item.Quality - 1 : item.Quality;
                        // Conjured Items & items that passed the sell date degrade their quality twice, 
                        // since it is already degreaded once, we degrade it one more time.                        
                        if (item.Name.Contains("Conjured") ^ item.SellIn <= 0)
                            item.Quality = item.Quality > 0 ? item.Quality - 1 : item.Quality;

                        // Question: What if the item is conjured and passed the sell by date, does it diminish four times the normal?
                        // twice since it is conjured item and quality degrades twice as fast once sell by date is passed.
                        // since it is already degraded once, we degrade it thrice to make it four times overall.
                        if (item.Name.Contains("Conjured") && item.SellIn <= 0)
                            item.Quality = (item.Quality - 3) > 0 ? item.Quality - 3 : 0;
                    }
                    else
                    {
                        // if it is a 'aged brie' then quality increases normally.
                        item.Quality = item.Quality < 50 ? item.Quality + 1 : 50;

                        if (item.Name.Contains("Backstage passes"))
                        {
                            //once sellin date passes, backstage passes quality drops to 0;
                            if (item.SellIn <= 0)
                                item.Quality -= item.Quality;

                            // if it is a 'backstage' & sellin is 10 or less, it increases twice, since it already increased once regardless of condition,
                            // it increases one more time to make it twice.
                            if (item.SellIn > 5 && item.SellIn < 11)
                                item.Quality = item.Quality < 50 ? item.Quality + 1 : 50;

                            // if it is a 'backstage' & sellin is 5 or less, it increases thrice, since it already increased once regardless of condition,
                            // it increases two more times to make it thrice.
                            if (item.SellIn > 0 && item.SellIn < 6)
                                item.Quality = (item.Quality + 2) > 50 ? 50 : item.Quality + 2;

                        }
                    }

                    ////Alternate Solution
                    //switch (item.Name)
                    //{
                    //    case "Aged Brie":
                    //        item.Quality = item.Quality < 50 ? item.Quality + 1 : 50;
                    //        break;
                    //    case string str when str.Contains("Backstage passes"):
                    //        item.Quality += 1;
                    //        if (item.SellIn <= 0)
                    //            item.Quality -= item.Quality;
                    //        if (item.SellIn > 5 && item.SellIn < 11)
                    //            item.Quality = item.Quality < 50 ? item.Quality + 1 : 50;
                    //        if (item.SellIn > 0 && item.SellIn < 6)
                    //            item.Quality = (item.Quality + 2) > 50 ? 50 : item.Quality + 2;
                    //        break;
                    //    case string str when str.Contains("Conjured"):
                    //        item.Quality = (item.Quality - 2) > 0 ? item.Quality - 2 : 0;
                    //        if (item.SellIn <= 0)
                    //            item.Quality = (item.Quality - 2) > 0 ? item.Quality - 2 : 0;
                    //        break;
                    //    default:
                    //        item.Quality = item.Quality > 0 ? item.Quality - 1 : 0;
                    //        break;
                    //}
                }
            }
            Console.WriteLine("Inventory update complete");
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }
    }
}