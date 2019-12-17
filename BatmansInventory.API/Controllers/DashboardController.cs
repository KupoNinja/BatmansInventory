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
        private readonly IPhysicalItemsRepository _pir;

        public DashboardController(IInventoryItemsService iis, IPhysicalItemsRepository pir)
        {
            _iis = iis;
            _pir = pir;
        }

        [HttpGet("[action]/{partNumber}")]
        public ActionResult<InventoryItem> GetByPartNumber(string partNumber)
        {
            try
            {
                return Ok(_iis.GetByPartNumber(partNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpGet("[action]/{serialNumber}")]
        public ActionResult<PhysicalItem> GetBySerialNumber(string serialNumber)
        {
            try
            {
                return Ok(_pir.GetBySerialNumber(serialNumber));
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
                return Ok(_pir.GetByLocation(locationId));
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
                return Ok(_pir.GetTotalValueByInventoryItem(inventoryItemId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}