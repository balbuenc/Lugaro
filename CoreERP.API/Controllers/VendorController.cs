using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IVendorRepository _VendorRepository;

        public VendorController(IVendorRepository vendorRepository)
        {
            _VendorRepository = vendorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            return Ok(await _VendorRepository.GetAllVendors());
        }

        [HttpGet]
        [Route("GetVendorsDDL")]
        public async Task<IActionResult> GetVendorsDDL()
        {
            return Ok(await _VendorRepository.GetVendorsDDL());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorDetails(int id)
        {
            return Ok(await _VendorRepository.GetVendorDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] Vendor vendor)
        {
            if (vendor == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _VendorRepository.InsertVendor(vendor);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateVendor([FromBody] Vendor vendor)
        {
            if (vendor == null)
                return BadRequest();

            if (vendor.proveedor.Trim() == string.Empty)
            {
                ModelState.AddModelError("Vendor", "Vendor name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _VendorRepository.UpdateVendor(vendor);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            if (id == 0)
                return BadRequest();

            await _VendorRepository.DeleteVendor(id);

            return NoContent(); //success
        }
    }
}
