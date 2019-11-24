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
            return _db.Items.ToList();
        }

        public ItemsService(DataContext db)
        {
            _db = db;
        }

    }
}