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
    public class GeneralPurchaseController : Controller
    {
        private readonly IGeneralPurchaseRepository _GeneralPurchaseRepository;

        public GeneralPurchaseController(IGeneralPurchaseRepository purchaseRepository)
        {
            _GeneralPurchaseRepository = purchaseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGeneralPurchases()
        {
            return Ok(await _GeneralPurchaseRepository.GetAllGeneralPurchases());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeneralPurchaseDetails(int id)
        {
            return Ok(await _GeneralPurchaseRepository.GetGeneralPurchaseDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGeneralPurchase([FromBody] GeneralPurchase purchase)
        {
            if (purchase == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _GeneralPurchaseRepository.InsertGeneralPurchase(purchase);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralPurchase([FromBody] GeneralPurchase purchase)
        {
            if (purchase == null)
                return BadRequest();



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _GeneralPurchaseRepository.UpdateGeneralPurchase(purchase);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralPurchase(int id)
        {
            if (id == 0)
                return BadRequest();

            await _GeneralPurchaseRepository.DeleteGeneralPurchase(id);

            return NoContent(); //success
        }
    }
}
