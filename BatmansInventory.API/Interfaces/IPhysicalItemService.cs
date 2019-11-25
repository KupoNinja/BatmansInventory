using BatmansInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IPhysicalItemService
    {
        List<PhysicalItem> GetAll();
        PhysicalItem GetById(int id);
        List<PhysicalItem> GetByLocation(int locationId);
        PhysicalItem GetBySerialNumber(string serialNumber);
        decimal GetTotalValueByItem(int itemId);
        PhysicalItem CreatePhysicalItem(PhysicalItem pItemData);
        PhysicalItem UpdatePhysicalItem(PhysicalItem pItemData);
        bool DeleteItem(int id);
    }
}
