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
    public class TransferController : Controller
    {
        private readonly ITransferRepository _TransferRepository;

        public TransferController(ITransferRepository accountTypeRepository)
        {
            _TransferRepository = accountTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransfers()
        {
            return Ok(await _TransferRepository.GetAllTransfers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferDetails(int id)
        {
            return Ok(await _TransferRepository.GetTransferDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] Transfer transfer)
        {
            if (transfer == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _TransferRepository.InsertTransfer(transfer);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransfer([FromBody] Transfer transfer)
        {
            if (transfer == null)
                return BadRequest();

            if (transfer.id_transferencia == 0)
            {
                ModelState.AddModelError("Transfer", "Transfer ID shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _TransferRepository.UpdateTransfer(transfer);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer(int id)
        {
            if (id == 0)
                return BadRequest();

            await _TransferRepository.DeleteTransfer(id);

            return NoContent(); //success
        }
    }
}
