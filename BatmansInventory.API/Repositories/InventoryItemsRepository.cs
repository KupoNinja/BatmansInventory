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
        private readonly BatmansInventoryContext _db;

        public InventoryItemsRepository(BatmansInventoryContext db)
        {
            _db = db;
        }

        public IEnumerable<InventoryItem> GetAll()
        {
            var inventoryItems = _db.InventoryItems.ToList();

            return inventoryItems;
        }

        public InventoryItem GetById(int id)
        {
            var inventoryItem = _db.InventoryItems.SingleOrDefault(i => i.InventoryItemId == id);

            return inventoryItem;
        }

        public IEnumerable<InventoryItem> GetAllUnderSafetyStock()
        {
            var inventoryItemsUnderSafetyStock = _db.InventoryItems.Where(i => i.QuantityOnHand < i.SafetyStock).ToList();

            return inventoryItemsUnderSafetyStock;
        }

        public InventoryItem GetByPartNumber(string partNumber)
        {
            var inventoryItems = _db.InventoryItems.SingleOrDefault(i => i.PartNumber == partNumber);

            return inventoryItems;
        }


        public InventoryItem CreateInventoryItem(InventoryItem inventoryItemToCreate)
        {
            _db.InventoryItems.Add(inventoryItemToCreate);
            _db.SaveChanges();

            return inventoryItemToCreate;
        }

        public InventoryItem UpdateInventoryItem(InventoryItem inventoryItemToUpdate)
        {
            _db.InventoryItems.Update(inventoryItemToUpdate);
            _db.SaveChanges();

            return inventoryItemToUpdate;
        }

        public bool DeleteInventoryItem(InventoryItem inventoryItemToDelete)
        {
            _db.InventoryItems.Remove(inventoryItemToDelete);
            _db.SaveChanges();

            return true;
        }

        public bool IsPartNumberDuplicate(string partNumber)
        {
            bool isDuplicated = _db.InventoryItems.Any(i => i.PartNumber == partNumber);

            return isDuplicated;
        }
    }
}