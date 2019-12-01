using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class InventoryItemsRepository : IInventoryItemsRepository
    {
        private readonly DataContext _db;

        public List<InventoryItem> GetAll()
        {
            var inventoryItems = _db.InventoryItems.ToList();
            if (inventoryItems == null || inventoryItems.Count == 0) { throw new Exception("There are no inventory items. Looks like a jokester wiped out his database. HaHAhA..."); }

            return inventoryItems;
        }

        public InventoryItem GetById(int id)
        {
            var inventoryItems = _db.InventoryItems.FirstOrDefault(i => i.InventoryItemId == id);
            if (inventoryItems == null) { throw new Exception("That inventory item doesn't exist. Might be a new item Lucius can invent!"); }

            return inventoryItems;
        }

        public List<InventoryItem> GetAllUnderSafetyStock()
        {
            //Think of a shorter name
            var inventoryItemsUnderSafetyStock = _db.InventoryItems.Where(i => i.QuantityOnHand < i.SafetyStock).ToList();

            return inventoryItemsUnderSafetyStock;
        }

        public InventoryItem GetByPartNumber(string partNumber)
        {
            var inventoryItems = _db.InventoryItems.FirstOrDefault(i => i.PartNumber == partNumber);
            if (inventoryItems == null) { throw new Exception("That inventory item doesn't exist. Might be a new item Lucius can invent!"); }

            return inventoryItems;
        }


        public InventoryItem CreateInventoryItem(InventoryItem inventoryItemData)
        {
            //Set Item VM or DTO for Create
            var newInventoryItem = new InventoryItem();
            newInventoryItem.PartName = inventoryItemData.PartName;
            //Validate for PartNumber convention... What's the convention?
            //Does not throw exception if PartNumber is not unique
            newInventoryItem.PartNumber = inventoryItemData.PartNumber;
            newInventoryItem.OrderLeadTime = inventoryItemData.OrderLeadTime;
            //Default QuantityOnHand to 0?
            newInventoryItem.QuantityOnHand = inventoryItemData.QuantityOnHand;
            newInventoryItem.SafetyStock = inventoryItemData.SafetyStock;
            newInventoryItem.Created = DateTime.Now;
            //Get UserId to auto Createdby
            newInventoryItem.CreatedBy = inventoryItemData.CreatedBy;

            _db.InventoryItems.Add(newInventoryItem);
            _db.SaveChanges();

            return newInventoryItem;
        }

        public InventoryItem UpdateInventoryItem(InventoryItem inventoryItemData)
        {
            //Change this to get by id
            var inventoryItemToUpdate = GetById(inventoryItemData.InventoryItemId);
            inventoryItemToUpdate.PartName = inventoryItemData.PartName;
            //How to handle if needing to change PartNumber?
            inventoryItemToUpdate.OrderLeadTime = inventoryItemData.OrderLeadTime;
            inventoryItemToUpdate.QuantityOnHand = inventoryItemData.QuantityOnHand;
            inventoryItemToUpdate.SafetyStock = inventoryItemData.SafetyStock;
            inventoryItemToUpdate.LastUpdated = DateTime.Now;
            inventoryItemToUpdate.LastUpdatedBy = inventoryItemData.LastUpdatedBy;

            //Added for disconnected state
            _db.InventoryItems.Update(inventoryItemToUpdate);
            _db.SaveChanges();

            return inventoryItemToUpdate;
        }

        public bool DeleteInventoryItem(int id)
        {
            var inventoryItemToDelete = GetById(id);

            _db.InventoryItems.Remove(inventoryItemToDelete);
            _db.SaveChanges();

            //Don't like this... Tired...
            return true;
        }

        public InventoryItemsRepository(DataContext db)
        {
            _db = db;
        }
    }
}