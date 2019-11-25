using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class PhysicalItemsService : IPhysicalItemService
    {
        private readonly DataContext _db;

        public List<PhysicalItem> GetAll()
        {
            var pItems = _db.PhysicalItems.ToList();
            if (pItems == null || pItems.Count == 0) { throw new Exception("Physical Item Inventory is empty. Looks like a jokester wiped out his database. HaHAhA..."); }

            return pItems;
        }

        public PhysicalItem GetById(int id)
        {
            var pitem = _db.PhysicalItems.FirstOrDefault(i => i.PhysicalItemId == id);
            if (pitem == null) { throw new Exception("That item doesn't exist. Looks like Alfred needs to order more!"); }

            return pitem;
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

        //Should be in class?
        public decimal GetTotalValueByItem(int itemId)
        {
            var pItemsCount = _db.PhysicalItems.Where(p => p.ItemId == itemId).ToList();

            return pItemsCount.Sum(p => p.Value);
        }

        public PhysicalItem CreatePhysicalItem(PhysicalItem pItemData)
        {
            //Set PhysicalItem VM or DTO for Create
            var newPItem = new PhysicalItem();
            //Should bring back Item object
            newPItem.ItemId = pItemData.ItemId;
            newPItem.SerialNumber = pItemData.SerialNumber;
            //Should bring back location object
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
            var pItemToUpdate = GetById(pItemData.PhysicalItemId);
            //Should have separate function to change ItemId for checks
            pItemToUpdate.ItemId = pItemData.ItemId;
            pItemToUpdate.SerialNumber = pItemData.SerialNumber;
            pItemToUpdate.LocationId = pItemData.LocationId;
            pItemToUpdate.Value = pItemData.Value;
            //How to handle if needing to change SerialNumber?
            pItemToUpdate.LastUpdated = DateTime.Now;
            pItemToUpdate.LastUpdatedBy = pItemData.LastUpdatedBy;

            //Added for disconnected state
            _db.PhysicalItems.Update(pItemToUpdate);
            _db.SaveChanges();

            return pItemToUpdate;
        }

        public bool DeleteItem(int id)
        {
            var pItemToDelete = GetById(id);

            _db.PhysicalItems.Remove(pItemToDelete);
            _db.SaveChanges();

            //Don't like this... Tired...
            return true;
        }

        public PhysicalItemsService(DataContext db)
        {
            _db = db;
        }
    }
}