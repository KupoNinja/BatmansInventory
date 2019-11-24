using BatmansInventory.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Models
{
    public class Item : IItem
    {
        public int ItemId { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int OrderLeadTime { get; set; }

        //get = count of physical items w/ itemid
        public int QuantityOnHand { get; set; }
        public int SafetyStock { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
#nullable enable
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }

        //SafetyStock method?
    }
}