using BatmansInventory.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatmansInventory.API.Models
{
    public class Location : ILocation
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }
}
