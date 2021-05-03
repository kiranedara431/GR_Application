using System.Collections.Generic;

namespace GR
{
    public interface IInventoryApp
    {
        void UpdateInventoryProcess(List<Item> Items);
        List<Item> LoadItems();
    }
}