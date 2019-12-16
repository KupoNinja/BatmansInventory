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
        private readonly IInventoryItemsService _iis;

        public InventoryItemsController(IInventoryItemsService iis)
        {
            _iis = iis;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> GetAll()
        {
            try
            {
                return Ok(_iis.GetAll());
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
                return Ok(_iis.GetByPartNumber(partNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<InventoryItem> GetById(int id)
        {
            try
            {
                return Ok(_iis.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<InventoryItem>> GetAllUnderSafetyStock()
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

        [HttpPost]
        public ActionResult<InventoryItem> CreateInventoryItem([FromBody] InventoryItem inventoryItemData)
        {
            try
            {
                //Set Item VM or DTO for Create
                InventoryItem newInventoryItem = _iis.CreateInventoryItem(inventoryItemData);

                //Unable to test Url
                return Created("api/inventoryitems/" + newInventoryItem.PartNumber, newInventoryItem);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<InventoryItem> UpdateInventoryItem(int id, [FromBody] InventoryItem inventoryItemData)
        {
            try
            {
                inventoryItemData.InventoryItemId = id;

                return Ok(_iis.UpdateInventoryItem(inventoryItemData));
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
                return Ok(_iis.DeleteInventoryItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}