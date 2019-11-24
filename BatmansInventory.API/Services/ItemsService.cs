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
            if (items == null) { throw new Exception("Item Inventory is empty. Looks like a funny villain wiped out his database. HaHAhA..."); }

            return items;
        }

        public Item GetByPartNumber(string partNumber)
        {
            var item = _db.Items.FirstOrDefault(i => i.PartNumber == partNumber);
            if (item == null) { throw new Exception("That item doesn't exist. Might be a new item Lucius can invent!"); }

            return item;
        }

        public ItemsService(DataContext db)
        {
            _db = db;
        }

    }
}