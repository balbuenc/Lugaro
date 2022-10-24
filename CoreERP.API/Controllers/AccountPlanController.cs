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
    public class AccountPlanController : Controller
    {
        private readonly IAccountPlanRepository _AccountPlanRepository;

        public AccountPlanController(IAccountPlanRepository accountTypeRepository)
        {
            _AccountPlanRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountPlans()
        {
            return Ok(await _AccountPlanRepository.GetAllAccountPlans());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountPlanDetails(int id)
        {
            return Ok(await _AccountPlanRepository.GetAccountPlanDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountPlan([FromBody] AccountPlan accountPlan)
        {
            if (accountPlan == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _AccountPlanRepository.InsertAccountPlan(accountPlan);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccountPlan([FromBody] AccountPlan accountPlan)
        {
            if (accountPlan == null)
                return BadRequest();

            if (accountPlan.cuenta.Trim() == string.Empty)
            {
                ModelState.AddModelError("AccountPlan", "AccountPlan name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _AccountPlanRepository.UpdateAccountPlan(accountPlan);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountPlan(int id)
        {
            if (id == 0)
                return BadRequest();

            await _AccountPlanRepository.DeleteAccountPlan(id);

            return NoContent(); //success
        }
    }
}
