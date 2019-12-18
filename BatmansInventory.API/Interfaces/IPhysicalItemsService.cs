using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IPhysicalItemsService
    {
        List<PhysicalItem> GetAll();
        PhysicalItem GetById(int id);
        List<PhysicalItem> GetByLocation(int locationId);
        PhysicalItem GetBySerialNumber(string serialNumber);
        decimal GetTotalValueByInventoryItem(int inventoryItemId);
        PhysicalItem CreatePhysicalItem(PhysicalItem pItemData);
        PhysicalItem UpdatePhysicalItem(PhysicalItem pItemData);
        bool DeletePhysicalItem(int id);
    }
}
