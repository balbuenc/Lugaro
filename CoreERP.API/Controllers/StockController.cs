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
    public class StockController : Controller
    {
        private readonly IStockRepository _StockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _StockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            return Ok(await _StockRepository.GetAllStocks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockDetails(int id)
        {
            return Ok(await _StockRepository.GetStockDetails(id));
        }

        [HttpGet]
        [Route("GetStockDetailsByStore/{ProductId}/{StoreID}")]
        public async Task<IActionResult> GetStockDetailsByStore(int productID, int storeID )
        {
            return Ok(await _StockRepository.GetStockDetailsByStore(productID, storeID));
        }

        [HttpGet]
        [Route("ProductStock/{id}")]
        public async Task<IActionResult> GetProductStock(int id)
        {
            return Ok(await _StockRepository.GetProductStock(id));
        }

        [HttpGet]
        [Route("GetTransferDestinations/{id}")]
        public async Task<IActionResult> GetTransferDestinations(int id)
        {
            return Ok(await _StockRepository.GetTransferDestinations(id));
        }


        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] Stock stock)
        {
            if (stock == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var created = await _StockRepository.InsertStock(stock);

            return Created("created", created);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromBody] Stock stock)
        {
            if (stock == null)
                return BadRequest();

            if (stock.id_deposito == 0)
            {
                ModelState.AddModelError("Stock", "Stock Deposit shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _StockRepository.UpdateStock(stock);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (id == 0)
                return BadRequest();

            await _StockRepository.DeleteStock(id);

            return NoContent(); //success
        }
    }
}
