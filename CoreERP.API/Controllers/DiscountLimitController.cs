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
    public class DiscountLimitController : Controller
    {
        private readonly IDiscountLimitRepository _DiscountLimitRepository;

        public DiscountLimitController(IDiscountLimitRepository accountTypeRepository)
        {
            _DiscountLimitRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscountLimits()
        {
            return Ok(await _DiscountLimitRepository.GetAllDiscountLimits());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountLimitDetails(int id)
        {
            return Ok(await _DiscountLimitRepository.GetDiscountLimitDetails(id));
        }

        [HttpGet]
        [Route("GetDiscountLimitsByBudgetID/{BudgetId}")]
        public async Task<IActionResult> GetDiscountLimitsByBudgetID(int BudgetId)
        {
            return Ok(await _DiscountLimitRepository.GetDiscountLimitsByBudgetID(BudgetId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountLimit([FromBody] DiscountLimit discountLimit)
        {
            if (discountLimit == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _DiscountLimitRepository.InsertDiscountLimit(discountLimit);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountLimit([FromBody] DiscountLimit discountLimit)
        {
            if (discountLimit == null)
                return BadRequest();

            if (discountLimit.marca.Trim() == string.Empty)
            {
                ModelState.AddModelError("DiscountLimit", "DiscountLimit Brand shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _DiscountLimitRepository.UpdateDiscountLimit(discountLimit);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountLimit(int id)
        {
            if (id == 0)
                return BadRequest();

            await _DiscountLimitRepository.DeleteDiscountLimit(id);

            return NoContent(); //success
        }
    }
}
