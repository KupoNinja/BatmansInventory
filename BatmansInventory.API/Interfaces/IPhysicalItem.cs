﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IPhysicalItem
    {
        int PhysicalItemId { get; set; }
        int InventoryItemId { get; set; }
        string SerialNumber { get; set; }
        int LocationId { get; set; }
        decimal Value { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }

#nullable enable
        DateTime? LastUpdated { get; set; }
        string? LastUpdatedBy { get; set; }
    }
}