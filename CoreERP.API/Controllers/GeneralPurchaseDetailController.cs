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
    public class GeneralPurchaseDetailController : Controller
    {
        private readonly IGeneralPurchaseDetailsRepository _GeneralPurchaseDetailsRepository;

        public GeneralPurchaseDetailController(IGeneralPurchaseDetailsRepository purchaseDetailsRepository)
        {
            _GeneralPurchaseDetailsRepository = purchaseDetailsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGeneralPurchaseDetailss()
        {
            return Ok(await _GeneralPurchaseDetailsRepository.GetAllGeneralPurchaseDetails());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeneralPurchaseDetails(int id)
        {
            return Ok(await _GeneralPurchaseDetailsRepository.GetGeneralPurchaseDetails(id));
        }




        [HttpPost]
        public async Task<IActionResult> CreateGeneralPurchaseDetails([FromBody] GeneralPurchaseDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _GeneralPurchaseDetailsRepository.InsertPurchaseDetail(budgetDetails);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateGeneralPurchaseDetails([FromBody] GeneralPurchaseDetails budgetDetails)
        {
            if (budgetDetails == null)
                return BadRequest();

            if (budgetDetails.monto.ToString() == null)
            {
                ModelState.AddModelError("GeneralPurchaseDetails", "GeneralPurchaseDetails price shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _GeneralPurchaseDetailsRepository.UpdatePurchaseDetail(budgetDetails);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneralPurchaseDetails(int id)
        {
            if (id == 0)
                return BadRequest();

            await _GeneralPurchaseDetailsRepository.DeletePurchaseDetail(id);

            return NoContent(); //success
        }

    }
}
