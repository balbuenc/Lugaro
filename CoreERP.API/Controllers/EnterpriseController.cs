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
    public class EnterpriseController : Controller
    {
        private readonly IEnterpriseRepository _EnterpriseRepository;

        public EnterpriseController(IEnterpriseRepository accountTypeRepository)
        {
            _EnterpriseRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnterprises()
        {
            return Ok(await _EnterpriseRepository.GetAllEnterprises());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnterpriseDetails(int id)
        {
            return Ok(await _EnterpriseRepository.GetEnterpriseDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnterprise([FromBody] Enterprise account)
        {
            if (account == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _EnterpriseRepository.InsertEnterprise(account);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEnterprise([FromBody] Enterprise account)
        {
            if (account == null)
                return BadRequest();

            if (account.empresa.Trim() == string.Empty)
            {
                ModelState.AddModelError("Enterprise", "Enterprise name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _EnterpriseRepository.UpdateEnterprise(account);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnterprise(int id)
        {
            if (id == 0)
                return BadRequest();

            await _EnterpriseRepository.DeleteEnterprise(id);

            return NoContent(); //success
        }
    }
}
