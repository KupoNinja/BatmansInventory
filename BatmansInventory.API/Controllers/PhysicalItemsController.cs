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
        private readonly IPhysicalItemsService _pis;

        public PhysicalItemsController(IPhysicalItemsService pis)
        {
            _pis = pis;
        }

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

        [HttpGet("{id}")]
        public ActionResult<PhysicalItem> GetById(int id)
        {
            try
            {
                return Ok(_pis.GetById(id));
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
                return Ok(_pis.DeletePhysicalItem(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}