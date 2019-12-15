using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatmansInventory.API.Interfaces;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IInventoryItemsRepository _iir;

        public InventoryItemsController(IInventoryItemsRepository iir)
        {
            _iir = iir;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> GetAll()
        {
            try
            {
                return Ok(_iir.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{partNumber}")]
        public ActionResult<InventoryItem> GetByPartNumber(string partNumber)
        {
            try
            {
                return Ok(_iir.GetByPartNumber(partNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Unable to test...
        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> GetAllUnderSafetyStock()
        {
            try
            {
                return Ok(_iir.GetAllUnderSafetyStock());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<InventoryItem> CreateInventoryItem([FromBody] InventoryItem inventoryItemData)
        {
            try
            {
                InventoryItem newInventoryItem = _iir.CreateInventoryItem(inventoryItemData);

                //Unable to test Url
                return Created("api/inventoryitems/" + newInventoryItem.PartNumber, newInventoryItem);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{partNumber}")]
        public ActionResult<InventoryItem> UpdateInventoryItem(string partNumber, [FromBody] InventoryItem inventoryItemData)
        {
            try
            {
                inventoryItemData.PartNumber = partNumber;

                return Ok(_iir.UpdateInventoryItem(inventoryItemData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(_iir.DeleteInventoryItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}