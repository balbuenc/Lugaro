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

    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clientRepository.GetAllClients());
        }


        [HttpGet]
        [Route("GetDefaultClientDetails")]
        public async Task<IActionResult> GetDefaultClientDetails()
        {
            return Ok(await _clientRepository.GetDefaultClientDetails());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            return Ok(await _clientRepository.GetClientDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (client == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _clientRepository.InsertClient(client);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            if (client == null)
                return BadRequest();

            if (client.nombres.Trim() == string.Empty)
            {
                ModelState.AddModelError("Name", "Client name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clientRepository.UpdateClient(client);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (id == 0)
                return BadRequest();

            await _clientRepository.DeleteClient(id);

            return NoContent(); //success
        }


    }
}
