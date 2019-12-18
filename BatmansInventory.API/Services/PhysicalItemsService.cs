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

            foreach (var pItem in pItems)
            {
                var returnedInventoryItem = FindInventoryItem(pItem.InventoryItemId);
                var returnedLocation = FindLocation(pItem.LocationId);

                pItem.InventoryItem = returnedInventoryItem;
                pItem.Location = returnedLocation;
            }

            return pItems;
        }

        public PhysicalItem GetById(int id)
        {
            var pItem = _pir.GetById(id);
            if (pItem == null) { throw new Exception("That item doesn't exist. Looks like Alfred needs to order more!"); }

            var returnedInventoryItem = FindInventoryItem(pItem.InventoryItemId);
            var returnedLocation = FindLocation(pItem.LocationId);

            pItem.InventoryItem = returnedInventoryItem;
            pItem.Location = returnedLocation;

            return pItem;
        }

        public List<PhysicalItem> GetByLocation(int locationId)
        {
            var pItems = _pir.GetByLocation(locationId);
            if (pItems == null || pItems.Count == 0) { throw new Exception("No items at this location. Let's hope kids didn't find your stash."); }

            foreach (var pItem in pItems)
            {
                var returnedInventoryItem = FindInventoryItem(pItem.InventoryItemId);
                var returnedLocation = FindLocation(pItem.LocationId);

                pItem.InventoryItem = returnedInventoryItem;
                pItem.Location = returnedLocation;
            }

            return pItems;
        }

        public PhysicalItem GetBySerialNumber(string serialNumber)
        {
            var pItem = _pir.GetBySerialNumber(serialNumber);
            if (pItem == null) { throw new Exception("Can't find anything with that serial number. Did the Riddler switch out your keyboard?"); }

            var returnedInventoryItem = FindInventoryItem(pItem.InventoryItemId);
            var returnedLocation = FindLocation(pItem.LocationId);

            pItem.InventoryItem = returnedInventoryItem;
            pItem.Location = returnedLocation;

            return pItem;
        }

        public decimal GetTotalValueByInventoryItem(int inventoryItemId)
        {
            var pItemsCount = _pir.GetTotalValueByInventoryItem(inventoryItemId);

            return pItemsCount;
        }

        public PhysicalItem CreatePhysicalItem(PhysicalItem pItemData)
        {            
            if (IsSerialNumberDuplicate(pItemData.SerialNumber)) { throw new Exception("Please try a different serial number."); }

            var returnedInventoryItem = FindInventoryItem(pItemData.InventoryItemId);
            var returnedLocation = FindLocation(pItemData.LocationId);
            

            //Set PhysicalItem DTO for Create
            var pItemToCreate = new PhysicalItem();
            pItemToCreate.InventoryItemId = pItemData.InventoryItemId;
            pItemToCreate.SerialNumber = pItemData.SerialNumber;
            pItemToCreate.LocationId = pItemData.LocationId;
            pItemToCreate.Value = pItemData.Value;
            pItemToCreate.Created = DateTime.Now;
            //Get UserId to auto CreatedBy
            pItemToCreate.CreatedBy = pItemData.CreatedBy;

            var createdPItem = _pir.CreatePhysicalItem(pItemToCreate);

            createdPItem.InventoryItem = returnedInventoryItem;
            createdPItem.Location = returnedLocation;

            return createdPItem;
        }

        public PhysicalItem UpdatePhysicalItem(PhysicalItem pItemData)
        {
            var returnedInventoryItem = FindInventoryItem(pItemData.InventoryItemId);
            var returnedLocation = FindLocation(pItemData.LocationId);

            var pItemToUpdate = GetById(pItemData.PhysicalItemId);
            pItemToUpdate.InventoryItemId = returnedInventoryItem.InventoryItemId;
            pItemToUpdate.InventoryItem = returnedInventoryItem;
            //Need to define what makes a serial number
            pItemToUpdate.SerialNumber = pItemData.SerialNumber;
            pItemToUpdate.LocationId = pItemData.LocationId;
            pItemToUpdate.Location = returnedLocation;
            pItemToUpdate.Value = pItemData.Value;
            pItemToUpdate.LastUpdated = DateTime.Now;
            pItemToUpdate.LastUpdatedBy = pItemData.LastUpdatedBy;

            var updatedPItem = _pir.UpdatePhysicalItem(pItemToUpdate);

            return updatedPItem;
        }

        public bool DeletePhysicalItem(int id)
        {
            var pItemToDelete = GetById(id);

            var isDeleted =_pir.DeletePhysicalItem(pItemToDelete);

            return isDeleted;
        }

        private bool IsSerialNumberDuplicate(string serialNumber)
        {
            var isDuplicated = _pir.IsSerialNumberDuplicate(serialNumber);

            return isDuplicated;
        }

        //Test these validations
        private InventoryItem FindInventoryItem(int inventoryItemId)
        {
            var inventoryItemToReturn = _iis.GetById(inventoryItemId);
            if (inventoryItemToReturn == null) { throw new Exception("We can't find that Inventory Item! Please try a different Inventory Item."); }

            return inventoryItemToReturn;
        }

        private Location FindLocation(int locationId)
        {
            var locationToReturn = _pir.GetLocation(locationId);
            if (locationToReturn == null) { throw new Exception("We can't find that location! Please try a different location."); }

            return locationToReturn;
        }
    }
}
