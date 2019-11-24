using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatmansInventory.API.Models;
using BatmansInventory.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalItemsController : ControllerBase
    {
        private readonly PhysicalItemsService _pis;

        [HttpGet]
        public ActionResult<IEnumerable<PhysicalItem>> GetAll()
        {
            try
            {
                return Ok(_pis.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{serialNumber}")]
        public ActionResult<Item> GetBySerialNumber(string serialNumber)
        {
            try
            {
                return Ok(_pis.GetBySerialNumber(serialNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<PhysicalItem> CreatePhysicalItem([FromBody] PhysicalItem pItemData)
        {
            try
            {
                PhysicalItem newItem = _pis.CreatePhysicalItem(pItemData);

                //Unable to test Url
                return Created("api/items/" + newItem.SerialNumber, newItem);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PhysicalItem> UpdatePhysicalItem(int id, [FromBody] PhysicalItem pItemData)
        {
            try
            {
                pItemData.PhysicalItemId = id;

                return Ok(_pis.UpdatePhysicalItem(pItemData));
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
                return Ok(_pis.DeleteItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public PhysicalItemsController(PhysicalItemsService pis)
        {
            _pis = pis;
        }
    }
}