using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IInventoryItemsService
    {
        List<InventoryItem> GetAll();
        InventoryItem GetById(int id);
        List<InventoryItem> GetAllUnderSafetyStock();
        InventoryItem GetByPartNumber(string partNumber);
        InventoryItem CreateInventoryItem(InventoryItem inventoryItemData);
        InventoryItem UpdateInventoryItem(InventoryItem inventoryItem);
        bool DeleteInventoryItem(int id);
    }
}
