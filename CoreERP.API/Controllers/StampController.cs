using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data.Repositories;
using CoreERP.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoreERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StampController : Controller
    {
        private readonly IStampRepository _StampRepository;

        public StampController(IStampRepository stampRepository)
        {
            _StampRepository = stampRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStamps()
        {
            return Ok(await _StampRepository.GetAllStamps());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStampDetails(int id)
        {
            return Ok(await _StampRepository.GetStampDetails(id));
        }

        [HttpGet("GetNextInvoiceNumber/{id}")]
        public async Task<IActionResult> GetNextInvoiceNumber(int id)
        {
            return Ok(await _StampRepository.GetNextInvoiceNumber(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStamp([FromBody] Stamp stamp)
        {
            if (stamp == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _StampRepository.InsertStamp(stamp);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateStamp([FromBody] Stamp stamp)
        {
            if (stamp == null)
                return BadRequest();

            if (stamp.timbrado.Trim() == string.Empty)
            {
                ModelState.AddModelError("Stamp", "Stamp number shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _StampRepository.UpdateStamp(stamp);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStamp(int id)
        {
            if (id == 0)
                return BadRequest();

            await _StampRepository.DeleteStamp(id);

            return NoContent(); //success
        }
    }
}
