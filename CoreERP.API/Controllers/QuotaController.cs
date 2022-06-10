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

    public class QuotaController : Controller
    {

        private readonly IQuotaRepository _QuotaRepository;

        public QuotaController(IQuotaRepository cuotaRepository)
        {
            _QuotaRepository = cuotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotas()
        {
            return Ok(await _QuotaRepository.GetAllQuotas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotaDetails(int id)
        {
            return Ok(await _QuotaRepository.GetQuotaDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuota([FromBody] Sale sale)
        {
            if (sale == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _QuotaRepository.GenerateSalesQuote(sale);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuota([FromBody] Quota cuota)
        {
            if (cuota == null)
                return BadRequest();

            if (cuota.id_cuota.ToString() == string.Empty)
            {
                ModelState.AddModelError("Quota", "Quota name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _QuotaRepository.UpdateQuota(cuota);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuota(int id)
        {
            if (id == 0)
                return BadRequest();

            await _QuotaRepository.DeleteQuota(id);

            return NoContent(); //success
        }


        [HttpDelete]
        [Route("DeleteQuotaBySaleId/{id}")]
        public async Task<IActionResult> DeleteQuotaBySaleId(int id)
        {
            if (id == 0)
                return BadRequest();

            await _QuotaRepository.DeleteQuotaBySaleId(id);

            return NoContent(); //success
        }

    }
}
