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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _EmployeeRepository.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetails(int id)
        {
            return Ok(await _EmployeeRepository.GetEmployeeDetails(id));
        }

        [HttpGet]
        [Route("GetUserId/{username}")]
        public async Task<IActionResult> GetEmployeeDetailsByUsername(string username)
        {
            return Ok(await _EmployeeRepository.GetEmployeeDetailsByUsername(username));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _EmployeeRepository.InsertEmployee(employee);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (employee.usuario.Trim() == string.Empty)
            {
                ModelState.AddModelError("Employee", "Employee name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _EmployeeRepository.UpdateEmployee(employee);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id == 0)
                return BadRequest();

            await _EmployeeRepository.DeleteEmployee(id);

            return NoContent(); //success
        }
    }
}
