using BatmansInventory.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Models
{
    public class PhysicalItem : IPhysicalItem
    {
        public int PhysicalItemId { get; set; }
        public int InventoryItemId { get; set; }
        public InventoryItem Item { get; set; }
        public string SerialNumber { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public decimal Value { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

#nullable enable
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}