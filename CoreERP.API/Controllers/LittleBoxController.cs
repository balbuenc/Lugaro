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
    public class LittleBoxController : Controller
    {
        private readonly ILittleBoxRepository _LittleBoxRepository;

        public LittleBoxController(ILittleBoxRepository littleBoxRepository)
        {
            _LittleBoxRepository = littleBoxRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLittleBoxs()
        {
            return Ok(await _LittleBoxRepository.GetAllLittleBox());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLittleBoxDetails(int id)
        {
            return Ok(await _LittleBoxRepository.GetLittleBoxDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLittleBox([FromBody] LittleBox littleBox)
        {
            if (littleBox == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _LittleBoxRepository.InsertLittleBox(littleBox);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLittleBox([FromBody] LittleBox littleBox)
        {
            if (littleBox == null)
                return BadRequest();

            if (littleBox.fecha_apertura.ToString().Trim() == string.Empty)
            {
                ModelState.AddModelError("LittleBox", "LittleBox date shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _LittleBoxRepository.UpdateLittleBox(littleBox);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLittleBox(int id)
        {
            if (id == 0)
                return BadRequest();

            await _LittleBoxRepository.DeleteLittleBox(id);

            return NoContent(); //success
        }
    }
}
