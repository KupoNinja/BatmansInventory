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
    public class PhysicalItemsController : ControllerBase
    {
        private readonly IPhysicalItemsRepository _pir;

        public PhysicalItemsController(IPhysicalItemsRepository pir)
        {
            _pir = pir;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PhysicalItem>> GetAll()
        {
            try
            {
                return Ok(_pir.GetAll());
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
                PhysicalItem newItem = _pir.CreatePhysicalItem(pItemData);

                //Unable to test Url
                return Created("api/physicalitems/" + newItem.SerialNumber, newItem);
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

                return Ok(_pir.UpdatePhysicalItem(pItemData));
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
                return Ok(_pir.DeletePhysicalItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}