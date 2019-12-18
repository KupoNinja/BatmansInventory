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
        private readonly BatmansInventoryContext _db;

        public PhysicalItemsRepository(BatmansInventoryContext db)
        {
            _db = db;
        }

        public List<PhysicalItem> GetAll()
        {
            var pItems = _db.PhysicalItems.ToList();
            if (pItems == null || pItems.Count == 0) { throw new Exception("Physical Item Inventory is empty. Looks like a jokester wiped out his database. HaHAhA..."); }

            return pItems;
        }

        public PhysicalItem GetById(int id)
        {
            var pItem = _db.PhysicalItems.FirstOrDefault(i => i.PhysicalItemId == id);
            if (pItem == null) { throw new Exception("That item doesn't exist. Looks like Alfred needs to order more!"); }

            return pItem;
        }

        public List<PhysicalItem> GetByLocation(int locationId)
        {
            var pItems = _db.PhysicalItems.Where(p => p.LocationId == locationId).ToList();
            if (pItems == null || pItems.Count == 0) { throw new Exception("No items at this location. Let's hope kids didn't find your stash."); }

            return pItems;
        }

        public PhysicalItem GetBySerialNumber(string serialNumber)
        {
            var pitem = _db.PhysicalItems.FirstOrDefault(p => p.SerialNumber == serialNumber);
            if (pitem == null) { throw new Exception("Can't find anything with that serial number. Did the Riddler switch out your keyboard?"); }

            return pitem;
        }

        public decimal GetTotalValueByInventoryItem(int inventoryItemId)
        {
            var pItemsCount = _db.PhysicalItems.Where(p => p.InventoryItemId == inventoryItemId).ToList();

            return pItemsCount.Sum(p => p.Value);
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

            _db.PhysicalItems.Add(newPItem);
            _db.SaveChanges();

            return newPItem;
        }

        public PhysicalItem UpdatePhysicalItem(PhysicalItem pItemData)
        {
            if (!IsInventoryItemIdValid(pItemData.InventoryItemId)) { throw new Exception("We can't find that Inventory Item! Please try a different Inventory Item."); }
            //Return InventoryItem

            var pItemToUpdate = GetById(pItemData.PhysicalItemId);
            pItemToUpdate.InventoryItemId = pItemData.InventoryItemId;
            //Need validation for SerialNumber
            pItemToUpdate.SerialNumber = pItemData.SerialNumber;
            pItemToUpdate.LocationId = pItemData.LocationId;
            pItemToUpdate.Value = pItemData.Value;
            pItemToUpdate.LastUpdated = DateTime.Now;
            pItemToUpdate.LastUpdatedBy = pItemData.LastUpdatedBy;

            _db.PhysicalItems.Update(pItemToUpdate);
            _db.SaveChanges();

            return pItemToUpdate;
        }

        public bool DeletePhysicalItem(int id)
        {
            var pItemToDelete = GetById(id);

            _db.PhysicalItems.Remove(pItemToDelete);
            _db.SaveChanges();

            return true;
        }

        private bool IsInventoryItemIdValid(int inventoryItemId)
        {
            if (!_db.InventoryItems.Any(i => i.InventoryItemId == inventoryItemId))
                return false;

            return true;
        }

        //private InventoryItem ReturnInventoryItem (PhysicalItem pItem)
        //{
        //    var inventoryItemToReturn = _db.InventoryItems.FirstOrDefault(i => i.InventoryItemId == pItem.InventoryItemId);
        //    if (inventoryItemToReturn == null) { throw new Exception("We can't find that Inventory Item! Please try a different Inventory Item."); }

        //    return inventoryItemToReturn;
        //}
    }
}
