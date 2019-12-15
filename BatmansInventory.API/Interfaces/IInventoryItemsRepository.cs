using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IInventoryItemsRepository
    {
        IEnumerable<InventoryItem> GetAll();
        InventoryItem GetById(int id);
        IEnumerable<InventoryItem> GetAllUnderSafetyStock();
        InventoryItem GetByPartNumber(string partNumber);
        InventoryItem CreateInventoryItem(InventoryItem inventoryItemToCreate);
        InventoryItem UpdateInventoryItem(InventoryItem inventoryItemToUpdate);
        bool DeleteInventoryItem(InventoryItem inventoryItemToDelete);
    }
}
