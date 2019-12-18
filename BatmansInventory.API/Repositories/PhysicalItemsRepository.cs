using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class PhysicalItemsRepository : IPhysicalItemsRepository
    {
        private readonly BatmansInventoryContext _db;

        public PhysicalItemsRepository(BatmansInventoryContext db)
        {
            _db = db;
        }

        public List<PhysicalItem> GetAll()
        {
            var pItems = _db.PhysicalItems.ToList();

            return pItems;
        }

        public PhysicalItem GetById(int id)
        {
            var pItem = _db.PhysicalItems.FirstOrDefault(i => i.PhysicalItemId == id);

            return pItem;
        }

        public List<PhysicalItem> GetByLocation(int locationId)
        {
            var pItems = _db.PhysicalItems.Where(p => p.LocationId == locationId).ToList();

            return pItems;
        }

        public PhysicalItem GetBySerialNumber(string serialNumber)
        {
            var pItem = _db.PhysicalItems.FirstOrDefault(p => p.SerialNumber == serialNumber);

            return pItem;
        }

        public decimal GetTotalValueByInventoryItem(int inventoryItemId)
        {
            var pItemsCount = _db.PhysicalItems.Where(p => p.InventoryItemId == inventoryItemId).ToList();

            return pItemsCount.Sum(p => p.Value);
        }

        public PhysicalItem CreatePhysicalItem(PhysicalItem pItemToCreate)
        {
            _db.PhysicalItems.Add(pItemToCreate);
            _db.SaveChanges();

            return pItemToCreate;
        }

        public PhysicalItem UpdatePhysicalItem(PhysicalItem pItemToUpdate)
        {
            _db.PhysicalItems.Update(pItemToUpdate);
            _db.SaveChanges();

            return pItemToUpdate;
        }

        public bool DeletePhysicalItem(PhysicalItem pItemToDelete)
        {
            _db.PhysicalItems.Remove(pItemToDelete);
            _db.SaveChanges();

            return true;
        }

        public Location GetLocation(int locationId)
        {
            var locationToReturn = _db.Locations.FirstOrDefault(l => l.LocationId == locationId);

            return locationToReturn;
        }
    }
}