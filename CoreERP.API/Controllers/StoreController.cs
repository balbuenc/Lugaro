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
    public class StoreController : Controller
    {
        private readonly IStoreRepository _StoreRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _StoreRepository = storeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            return Ok(await _StoreRepository.GetAllStores());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreDetails(int id)
        {
            return Ok(await _StoreRepository.GetStoreDetails(id));
        }

        [HttpGet]
        [Route("GetStoreDetailsByName/{name}")]
        public async Task<IActionResult> GetStoreDetailsByName(string name)
        {
            return Ok(await _StoreRepository.GetStoreDetailsByName(name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] Store store)
        {
            if (store == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _StoreRepository.InsertStore(store);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] Store store)
        {
            if (store == null)
                return BadRequest();

            if (store.deposito.Trim() == string.Empty)
            {
                ModelState.AddModelError("Store", "Store name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _StoreRepository.UpdateStore(store);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (id == 0)
                return BadRequest();

            await _StoreRepository.DeleteStore(id);

            return NoContent(); //success
        }

    }

}
