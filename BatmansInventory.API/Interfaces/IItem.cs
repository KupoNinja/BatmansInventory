using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface IItem
    {
        int ItemId { get; set; }
        string PartName { get; set; }
        string PartNumber { get; set; }
        int OrderLeadTime { get; set; }
        int QuantityOnHand { get; set; }
        int SafetyStock { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }

//C#8 Warning if 
#nullable enable
        DateTime? LastUpdated { get; set; }
        string? LastUpdatedBy { get; set; }
    }
}