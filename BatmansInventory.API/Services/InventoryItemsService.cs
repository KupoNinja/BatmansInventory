using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class InventoryItemsService : IInventoryItemsService
    {
        private readonly IInventoryItemsRepository _iir;

        public InventoryItemsService(IInventoryItemsRepository iir)
        {
            _iir = iir;
        }

        public List<InventoryItem> GetAll()
        {
            var inventoryItemsList = _iir.GetAll().ToList();
            if (inventoryItemsList == null) { throw new Exception("There are no inventory items. Looks like a jokester wiped out his database. HaHAhA..."); }

            return inventoryItemsList;
        }

        public InventoryItem GetById(int id)
        {
            var inventoryItem = _iir.GetById(id);
            if (inventoryItem == null) { throw new Exception("That inventory item doesn't exist. Might be a new item Lucius can invent!"); }

            return inventoryItem;
        }

        public List<InventoryItem> GetAllUnderSafetyStock()
        {
            var inventoryItemsUnderSafetyStock = _iir.GetAllUnderSafetyStock().ToList();
            if (inventoryItemsUnderSafetyStock == null) { throw new Exception("There are no items under safety stock. Looks like we're well equipped for battle!"); }

            return inventoryItemsUnderSafetyStock;
        }
        public InventoryItem GetByPartNumber(string partNumber)
        {
            var inventoryItem = _iir.GetByPartNumber(partNumber);
            if (inventoryItem == null) { throw new Exception("We can't find that part number. If your head hasn't been hit recently maybe you misspelled it!"); }

            return inventoryItem;
        }

        public InventoryItem CreateInventoryItem(InventoryItem inventoryItemData)
        {
            var inventoryItemToCreate = new InventoryItem();
            inventoryItemToCreate.PartName = inventoryItemData.PartName;
            //Validate for PartNumber convention... What's the convention?
            //Does not throw exception if PartNumber is not unique
            inventoryItemToCreate.PartNumber = inventoryItemData.PartNumber;
            inventoryItemToCreate.OrderLeadTime = inventoryItemData.OrderLeadTime;
            //Default QuantityOnHand to 0?
            inventoryItemToCreate.QuantityOnHand = inventoryItemData.QuantityOnHand;
            inventoryItemToCreate.SafetyStock = inventoryItemData.SafetyStock;
            inventoryItemToCreate.Created = DateTime.Now;
            //Get UserId to auto Createdby
            inventoryItemToCreate.CreatedBy = inventoryItemData.CreatedBy;

            var createdInventoryItem = _iir.CreateInventoryItem(inventoryItemToCreate);

            return createdInventoryItem;
        }

        public InventoryItem UpdateInventoryItem(InventoryItem inventoryItem)
        {
            var inventoryItemToUpdate = GetById(inventoryItem.InventoryItemId);

            inventoryItemToUpdate.PartName = inventoryItem.PartName;
            //How to handle if needing to change PartNumber?
            inventoryItemToUpdate.PartNumber = inventoryItem.PartNumber;
            inventoryItemToUpdate.OrderLeadTime = inventoryItem.OrderLeadTime;
            inventoryItemToUpdate.QuantityOnHand = inventoryItem.QuantityOnHand;
            inventoryItemToUpdate.SafetyStock = inventoryItem.SafetyStock;
            inventoryItemToUpdate.LastUpdated = DateTime.Now;
            inventoryItemToUpdate.LastUpdatedBy = inventoryItem.LastUpdatedBy;

            var updatedInventoryItem = _iir.UpdateInventoryItem(inventoryItemToUpdate);

            return updatedInventoryItem;
        }
        public bool DeleteInventoryItem(int id)
        {
            var inventoryItemToDelete = GetById(id);

            var isDeleted = _iir.DeleteInventoryItem(inventoryItemToDelete);

            return isDeleted;
        }
    }
}
