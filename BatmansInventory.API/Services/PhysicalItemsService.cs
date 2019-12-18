using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class PhysicalItemsService : IPhysicalItemsService
    {
        private readonly IPhysicalItemsRepository _pir;
        private readonly IInventoryItemsRepository _iis;

        public PhysicalItemsService(IPhysicalItemsRepository pir, IInventoryItemsRepository iis)
        {
            _pir = pir;
            _iis = iis;
        }

        public List<PhysicalItem> GetAll()
        {
            var pItems = _pir.GetAll();
            if (pItems == null || pItems.Count == 0) { throw new Exception("Physical Item Inventory is empty. Looks like a jokester wiped out his database. HaHAhA..."); }

            return pItems;
        }

        public PhysicalItem GetById(int id)
        {
            var pItem = _pir.GetById(id);
            if (pItem == null) { throw new Exception("That item doesn't exist. Looks like Alfred needs to order more!"); }

            return pItem;
        }

        public List<PhysicalItem> GetByLocation(int locationId)
        {
            var pItems = _pir.GetByLocation(locationId);
            if (pItems == null || pItems.Count == 0) { throw new Exception("No items at this location. Let's hope kids didn't find your stash."); }

            return pItems;
        }

        public PhysicalItem GetBySerialNumber(string serialNumber)
        {
            var pitem = _pir.GetBySerialNumber(serialNumber);
            if (pitem == null) { throw new Exception("Can't find anything with that serial number. Did the Riddler switch out your keyboard?"); }

            return pitem;
        }

        public decimal GetTotalValueByInventoryItem(int inventoryItemId)
        {
            var pItemsCount = _pir.GetTotalValueByInventoryItem(inventoryItemId);

            return pItemsCount;
        }

        public PhysicalItem CreatePhysicalItem(PhysicalItem pItemData)
        {
            //Set PhysicalItem DTO for Create
            var newPItem = new PhysicalItem();
            //Should bring back Item object
            newPItem.InventoryItemId = pItemData.InventoryItemId;
            newPItem.SerialNumber = pItemData.SerialNumber;
            //Should bring back Location object
            newPItem.LocationId = pItemData.LocationId;
            newPItem.Value = pItemData.Value;
            newPItem.Created = DateTime.Now;
            //Get UserId to auto Createdby
            newPItem.CreatedBy = pItemData.CreatedBy;

            var createdPItem = _pir.CreatePhysicalItem(pItemData);

            return createdPItem;
        }

        public PhysicalItem UpdatePhysicalItem(PhysicalItem pItemData)
        {
            var returnedInventoryItem = ReturnInventoryItem(pItemData.InventoryItemId);
            var returnedLocation = ReturnLocation(pItemData.LocationId);

            var pItemToUpdate = GetById(pItemData.PhysicalItemId);
            pItemToUpdate.InventoryItemId = returnedInventoryItem.InventoryItemId;
            pItemToUpdate.Item = returnedInventoryItem;
            //Need validation for SerialNumber
            pItemToUpdate.SerialNumber = pItemData.SerialNumber;
            pItemToUpdate.LocationId = pItemData.LocationId;
            pItemToUpdate.Value = pItemData.Value;
            pItemToUpdate.LastUpdated = DateTime.Now;
            pItemToUpdate.LastUpdatedBy = pItemData.LastUpdatedBy;

            var updatedPItem = _pir.UpdatePhysicalItem(pItemToUpdate);

            return updatedPItem;
        }

        public bool DeletePhysicalItem(int id)
        {
            var pItemToDelete = GetById(id);

            var isDeleted =_pir.DeletePhysicalItem(id);

            return isDeleted;
        }

        private InventoryItem ReturnInventoryItem(int inventoryItemId)
        {
            var inventoryItemToReturn = _iis.GetById(inventoryItemId);
            if (inventoryItemToReturn == null) { throw new Exception("We can't find that Inventory Item! Please try a different Inventory Item."); }

            return inventoryItemToReturn;
        }

        private Location ReturnLocation(int locationId)
        {
            var locationToReturn = _pir.GetLocation(locationId);
            if (locationToReturn == null) { throw new Exception("We can't find that location! Please try a different location."); }

            return locationToReturn;
        }
    }
}
