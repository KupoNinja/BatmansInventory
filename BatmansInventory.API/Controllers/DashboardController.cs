using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BatmansInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IInventoryItemsService _iis;
        private readonly IPhysicalItemsService _pis;

        public DashboardController(IInventoryItemsService iis, IPhysicalItemsService pis)
        {
            _iis = iis;
            _pis = pis;
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<InventoryItem>> GetAllInventoryItemsUnderSafetyStock()
        {
            try
            {
                return Ok(_iis.GetAllUnderSafetyStock());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{locationId}")]
        public ActionResult<IEnumerable<PhysicalItem>> GetByLocation(int locationId)
        {
            try
            {
                return Ok(_pis.GetByLocation(locationId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{inventoryItemId}")]
        public ActionResult<decimal> GetTotalValueByItem(int inventoryItemId)
        {
            try
            {
                return Ok(_pis.GetTotalValueByInventoryItem(inventoryItemId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}