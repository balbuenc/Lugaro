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
    public class LittleBoxDetailsController : Controller
    {
        private readonly ILittleBoxDetailRepository _LittleBoxDetailRepository;

        public LittleBoxDetailsController(ILittleBoxDetailRepository littleBoxDeatilsRepository)
        {
            _LittleBoxDetailRepository = littleBoxDeatilsRepository;
        }

        //[HttpGet("{accountID}")]
        //public async Task<IActionResult> GetAllLittleBoxDetailss(int accountID)
        //{
        //    return Ok(await _LittleBoxDetailRepository.GetAllLittleBoxDetailss(accountID));
        //}

        [HttpGet("{LittleBoxID}")]
        public async Task<IActionResult> GetLittleBoxDetailsDetails(int LittleBoxID)
        {
            return Ok(await _LittleBoxDetailRepository.GetAllLittleBoxDetails(LittleBoxID));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLittleBoxDetails([FromBody] LittleBoxDetails littleBoxDetail)
        {
            if (littleBoxDetail == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _LittleBoxDetailRepository.InsertLittleBoxDetail(littleBoxDetail);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLittleBoxDetails([FromBody] LittleBoxDetails littleBoxDetail)
        {
            if (littleBoxDetail == null)
                return BadRequest();

            if (littleBoxDetail.monto.ToString().Trim() == string.Empty)
            {
                ModelState.AddModelError("LittleBoxDetails", "Account number shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _LittleBoxDetailRepository.UpdateLittleBoxDetail(littleBoxDetail);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLittleBoxDetails(int id)
        {
            if (id == 0)
                return BadRequest();

            await _LittleBoxDetailRepository.DeleteLittleBoxDetail(id);

            return NoContent(); //success
        }
    }
}
