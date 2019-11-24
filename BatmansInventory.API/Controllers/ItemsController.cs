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
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _its;

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            try
            {
                return Ok(_its.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{partNumber}")]
        public ActionResult<Item> GetByPartNumber(string partNumber)
        {
            try
            {
                return Ok(_its.GetByPartNumber(partNumber));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Unable to test...
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAllUnderSafetyStock()
        {
            try
            {
                return Ok(_its.GetAllUnderSafetyStock());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Item> CreateItem([FromBody] Item itemData)
        {
            try
            {
                Item newItem = _its.CreateItem(itemData);

                //Unable to test Url
                return Created("api/items/" + newItem.PartNumber, newItem);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{partNumber}")]
        public ActionResult<Item> UpdateItem(string partNumber, [FromBody] Item itemData)
        {
            try
            {
                itemData.PartNumber = partNumber;

                return Ok(_its.UpdateItem(itemData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public ItemsController(ItemsService its)
        {
            _its = its;
        }
    }
}
