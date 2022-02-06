﻿using System;
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
    public class BudgetController : Controller
    {
        private readonly IBudgetRepository _BudgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            _BudgetRepository = budgetRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBudgets()
        {
            return Ok(await _BudgetRepository.GetAllBudgets());
        }

        [HttpGet]
        [Route("GetAllBudgetsByUserName/{userName}/{canViewOnlyOwned}")]
        public async Task<IActionResult> GetAlGetAllBudgetsByUserName(string userName, bool canViewOnlyOwned)
        {
            return Ok(await _BudgetRepository.GetAllBudgetsByUserName(userName, canViewOnlyOwned));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetDetails(int id)
        {
            return Ok(await _BudgetRepository.GetBudgetDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] Budget budget)
        {
            if (budget == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _BudgetRepository.InsertBudget(budget);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBudget([FromBody] Budget budget)
        {
            if (budget == null)
                return BadRequest();

            if (budget.estado.Trim() == string.Empty)
            {
                ModelState.AddModelError("Budget", "Budget status shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _BudgetRepository.UpdateBudget(budget);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            if (id == 0)
                return BadRequest();

            await _BudgetRepository.DeleteBudget(id);

            return NoContent(); //success
        }
    }
}
