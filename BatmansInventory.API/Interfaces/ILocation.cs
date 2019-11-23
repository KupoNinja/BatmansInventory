using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Interfaces
{
    public interface ILocation
    {
        int LocationId { get; set; }
        string City { get; set; }
        string State { get; set; }
    }
}
