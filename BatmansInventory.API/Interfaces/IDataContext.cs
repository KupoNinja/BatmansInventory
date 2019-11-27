using BatmansInventory.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IDataContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<PhysicalItem> PhysicalItems { get; set; }
        DbSet<Location> Locations { get; set; }
    }
}
