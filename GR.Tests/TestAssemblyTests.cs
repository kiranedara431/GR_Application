using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Unity;

namespace GR.Tests
{
    public class TestAssemblyTests
    {
        public List<Item> Items { get; set; }

        //private readonly InventoryApp _inventoryApp;
        //private Mock<IUpdateInventory> _updateInventoryMock = new Mock<IUpdateInventory>();
        public TestAssemblyTests()
        {
            // tried mocking the dependency, but it couldn't enter the update method, 
            // so used unity to register the dependency just like did it in the main program
            var container = new UnityContainer();
            container.RegisterType<IUpdateInventory, UpdateInventory>();

            var inventoryApp = container.Resolve<InventoryApp>();

            Items = LoadItems();
            inventoryApp.UpdateInventoryProcess(Items);



            //_updateInventoryMock.Setup(x=>x.Update(It.IsAny<List<Item>>())).Verifiable();

            //_inventoryApp = new InventoryApp(_updateInventoryMock.Object);
            //_inventoryApp.UpdateInventoryProcess(Items);

        }

        private List<Item> LoadItems()
        {
            var Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 1},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a D498FJ9FJ2N concert",
                    SellIn = 10,
                    Quality = 30
                },
                new Item
                {
                    Name = "Backstage passes to a FH38F39DJ39 concert",
                    SellIn = 5,
                    Quality = 33
                },
                new Item
                {
                    Name = "Backstage passes to a I293JD92J44 concert",
                    SellIn = 6,
                    Quality = 27
                },
                new Item
                {
                    Name = "Backstage passes to a O2848394820 concert",
                    SellIn = 1,
                    Quality = 13
                },
                new Item
                {
                    Name = "Backstage passes to a DEEEADMEEET concert",
                    SellIn = 0,
                    Quality = 25
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            return Items;
        }

        [Fact]
        public void DexterityVest_SellIn_ShouldDecreaseByOne()
        {

            Assert.Equal(9, Items.First(x => x.Name == "+5 Dexterity Vest").SellIn);
        }

        [Fact]
        public void DexterityVest_Quality_ShouldDecreaseByOne()
        {
            Assert.Equal(19, Items.First(x => x.Name == "+5 Dexterity Vest").Quality);
        }

        [Fact]
        public void Conjured_SellIn_ShouldDecreaseByOne()
        {
            Assert.Equal(2, Items.First(x => x.Name == "Conjured Mana Cake").SellIn);
        }

        [Fact]
        public void Conjured_Quality_ShouldDecreaseByTwo()
        {
            Assert.Equal(4, Items.First(x => x.Name == "Conjured Mana Cake").Quality);
        }
    }
}