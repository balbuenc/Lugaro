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
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository _PurchaseRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository)
        {
            _PurchaseRepository = purchaseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            return Ok(await _PurchaseRepository.GetAllPurchases());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseDetails(int id)
        {
            return Ok(await _PurchaseRepository.GetPurchaseDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] Purchase purchase)
        {
            if (purchase == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _PurchaseRepository.InsertPurchase(purchase);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePurchase([FromBody] Purchase purchase)
        {
            if (purchase == null)
                return BadRequest();

           

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _PurchaseRepository.UpdatePurchase(purchase);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            if (id == 0)
                return BadRequest();

            await _PurchaseRepository.DeletePurchase(id);

            return NoContent(); //success
        }
    }
}
