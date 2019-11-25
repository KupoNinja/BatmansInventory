using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IItemsService
    {
        List<Item> GetAll();
        Item GetById(int id);
        List<Item> GetAllUnderSafetyStock();
        Item GetByPartNumber(string partNumber);
        Item CreateItem(Item itemData);
        Item UpdateItem(Item itemData);
        bool DeleteItem(int id);
    }
}
