using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Services
{
    public class ItemsService 
    {
        private readonly DataContext _db;

        public List<Item> GetAll()
        {
            var items = _db.Items.ToList();
            if (items == null || items.Count == 0) { throw new Exception("Item Inventory is empty. Looks like a jokester wiped out his database. HaHAhA..."); }

            return items;
        }

        private Item GetById(int id)
        {
            var item = _db.Items.FirstOrDefault(i => i.ItemId == id);
            if (item == null) { throw new Exception("That item doesn't exist. Might be a new item Lucius can invent!"); }

            return item;
        }

        public List<Item> GetAllUnderSafetyStock()
        {
            var itemsUnderSafetyStock = _db.Items.Where(i => i.QuantityOnHand < i.SafetyStock).ToList();

            return itemsUnderSafetyStock;
        }

        public Item GetByPartNumber(string partNumber)
        {
            var item = _db.Items.FirstOrDefault(i => i.PartNumber == partNumber);
            if (item == null) { throw new Exception("That item doesn't exist. Might be a new item Lucius can invent!"); }

            return item;
        }


        public Item CreateItem(Item itemData)
        {
            //Set Item VM or DTO for Create
            var newItem = new Item();
            newItem.PartName = itemData.PartName;
            //Validate for PartNumber convention... What's the convention?
            //Does not throw exception if PartNumber is not unique
            newItem.PartNumber = itemData.PartNumber;
            newItem.OrderLeadTime = itemData.OrderLeadTime;
            //Default QuantityOnHand to 0?
            newItem.QuantityOnHand = itemData.QuantityOnHand;
            newItem.SafetyStock = itemData.SafetyStock;
            newItem.Created = DateTime.Now;
            //Get UserId to auto Createdby
            newItem.CreatedBy = itemData.CreatedBy;

            _db.Items.Add(newItem);
            _db.SaveChanges();

            return newItem;
        }

        public Item UpdateItem(Item itemData)
        {
            //Change this to get by id
            var itemToUpdate = GetById(itemData.ItemId);
            itemToUpdate.PartName = itemData.PartName;
            //How to handle if needing to change PartNumber?
            itemToUpdate.OrderLeadTime = itemData.OrderLeadTime;
            itemToUpdate.QuantityOnHand = itemData.QuantityOnHand;
            itemToUpdate.SafetyStock = itemData.SafetyStock;
            itemToUpdate.LastUpdated = DateTime.Now;
            itemToUpdate.LastUpdatedBy = itemData.LastUpdatedBy;

            //Added for disconnected state
            _db.Items.Update(itemToUpdate);
            _db.SaveChanges();

            return itemToUpdate;
        }

        public bool DeleteItem(int id)
        {
            var itemToDelete = GetById(id);

            _db.Items.Remove(itemToDelete);
            _db.SaveChanges();

            //Don't like this... Tired...
            return true;
        }

        public ItemsService(DataContext db)
        {
            _db = db;
        }
    }
}